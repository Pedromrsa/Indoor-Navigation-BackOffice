// Função para carregar os scripts
let vectorLayer;
function loadScripts() {
    var olStylesheet = document.createElement('link');
    olStylesheet.rel = 'stylesheet';
    olStylesheet.href = 'https://cdn.jsdelivr.net/npm/ol@v10.5.0/ol.css';
    olStylesheet.onload = function () {
        console.log('OpenLayers CSS carregado!');
    };
    olStylesheet.onerror = function () {
        console.error('Erro ao carregar o CSS do OpenLayers!');
    };
    document.head.appendChild(olStylesheet);


    var openLayersScript = document.createElement('script');
    openLayersScript.src = 'https://cdn.jsdelivr.net/npm/ol@v10.5.0/dist/ol.js';
    openLayersScript.type = 'text/javascript';
    openLayersScript.onload = function () {
        console.log('OpenLayers JS carregado!');
        initMap(); 
    };
    openLayersScript.onerror = function () {
        console.error('Erro ao carregar o JS do OpenLayers!');
    };
    document.head.appendChild(openLayersScript);
}

// Função para inicializar o mapa
function initMap() {
    console.log('Inicializando o mapa...');

    // Variável para o nível selecionado
    let nivelSelecionado = '1';

    // Estilo padrão
    const estiloPadrao = new ol.style.Style({
        stroke: new ol.style.Stroke({ color: '#444', width: 2 }),
        fill: new ol.style.Fill({ color: 'rgba(200, 200, 200, 0.3)' }),
        image: new ol.style.Circle({
            radius: 5,
            fill: new ol.style.Fill({ color: '#444' })
        })
    });

    // Fonte de dados GeoJSON
    const vectorSource = new ol.source.Vector({
        url: '/data/indoor_map.geojson',
        format: new ol.format.GeoJSON()
    });

    // Camada vetorial com estilo dinâmico por tipo e nível
    const vectorLayer = new ol.layer.Vector({
        source: vectorSource,
        style: function (feature) {
            const level = feature.get('level');
            const levelList = level ? level.split(';') : [];
            if (!levelList.includes(nivelSelecionado)) return null;

            // Portas
            if (feature.get('door')) {
                return new ol.style.Style({
                    image: new ol.style.Icon({
                        src: '/imagens/entrance_door_icon.png',
                        scale: 0.03
                    })
                });
            }

            // Janelas
            if (feature.get('window') === 'glass') {
                return new ol.style.Style({
                    stroke: new ol.style.Stroke({ color: '#00BFFF', width: 3 })
                });
            }

            // Entradas
            if (feature.get('entrance')) {
                return new ol.style.Style({
                    image: new ol.style.Icon({
                        src: '/imagens/entrance_icon.png',
                        scale: 0.01
                    })
                });
            }

            // Paredes
            if (feature.get('indoor') === 'wall') {
                const material = feature.get('material');
                const fillColor = material === 'glass' ? '#00BFFF' : '#a49f9f';
                return new ol.style.Style({
                    stroke: new ol.style.Stroke({ color: '#000000', width: 1 }),
                    fill: new ol.style.Fill({ color: fillColor })
                });
            }

            // Salas
            if (feature.get('indoor') === 'room') {
                return new ol.style.Style({
                    stroke: new ol.style.Stroke({ color: '#a49f9f', width: 1 }),
                    fill: new ol.style.Fill({ color: '#ffffa1' })
                });
            }

            // Corredores
            if (feature.get('indoor') === 'corridor') {
                return new ol.style.Style({
                    fill: new ol.style.Fill({ color: '#eeeeee' }),
                    stroke: new ol.style.Stroke({ color: '#999999', width: 1 })
                });
            }

            // Escadas
            if (feature.get('highway') === 'steps') {
                return new ol.style.Style({
                    stroke: new ol.style.Stroke({
                        color: '#000000',
                        width: 1,
                        lineDash: [15, 5]
                    }),
                    fill: new ol.style.Fill({ color: '#a6ff96' })
                });
            }

            if (feature.get('highway') === 'elevator') {
                var geometry = feature.getGeometry();
                var center = geometry.getInteriorPoint().getCoordinates();

                // Estilo para o polígono (preenchimento e borda)
                var polygonStyle = new ol.style.Style({
                    stroke: new ol.style.Stroke({
                        color: '#000000',
                        width: 1,
                        lineDash: [5, 5]  // Linha tracejada
                    }),
                    fill: new ol.style.Fill({
                        color: '#C3E5FA'  // Cor de preenchimento
                    })
                });

                // Estilo para o ícone no centro do polígono
                var iconStyle = new ol.style.Style({
                    image: new ol.style.Icon({
                        src: '/imagens/elevator_icon.png',
                        scale: 0.05,
                        anchor: [0.5, 0.5],  // Centraliza o ícone no centro do polígono
                        anchorXUnits: 'fraction',
                        anchorYUnits: 'fraction'
                    }),
                    geometry: new ol.geom.Point(center)  // Coloca o ícone no centro do polígono
                });

                // Retorna um array com os dois estilos: o do polígono e o do ícone
                return [polygonStyle, iconStyle];
            }

            // Padrão
            return estiloPadrao;
        }
    });

    // Inicialização do mapa
    const map = new ol.Map({
        target: 'map',
        layers: [
            new ol.layer.Tile({
                source: new ol.source.TileImage({
                    tileUrlFunction: () => "",
                    tileLoadFunction: () => { }
                })
            }),
            vectorLayer
        ],
        view: new ol.View({
            center: ol.proj.fromLonLat([-8.6077, 41.1778]),
            zoom: 0
        })
    });

    // Zoom automático ao carregar dados
    vectorSource.on('change', function () {
        if (vectorSource.getState() === 'ready') {
            map.getView().fit(vectorSource.getExtent(), {
                padding: [50, 50, 50, 50],
                maxZoom: 21
            });
        }
    });

    // Listener para o seletor de andar
    const levelSelector = document.getElementById('level');
    if (levelSelector) {
        levelSelector.value = '1';
        levelSelector.addEventListener('change', function (e) {
            nivelSelecionado = e.target.value;
            vectorLayer.changed(); // força redesenho
        });
    }

    // Se houver outro seletor (ex: <select id="nivelSelector">)
    const select = document.getElementById('nivelSelector');
    if (select) {
        select.addEventListener('change', function () {
            nivelSelecionado = this.value;
            vectorLayer.changed();
        });
    }

    console.log('Mapa inicializado com sucesso!');
}

    

