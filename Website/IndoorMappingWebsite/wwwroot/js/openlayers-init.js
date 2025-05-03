// Função para carregar os scripts
let vectorLayer;
let vectorSource;
let clickCoordinates = [];
let levelSelector;
let caminhosSource, caminhosLayer;
let map;
let mapClickListener = null;
let draw;
let coordinate;

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
    vectorSource = new ol.source.Vector({
        url: '/data/indoor_map.geojson',
        format: new ol.format.GeoJSON()
    });

    // Camada vetorial com estilo dinâmico por tipo e nível
    vectorLayer = new ol.layer.Vector({
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

            if (feature.get('indoor') === 'room') {
                // Estilo da sala (borda e preenchimento)
                let styles = [
                    new ol.style.Style({
                        stroke: new ol.style.Stroke({
                            color: '#a49f9f',  // Cor da borda
                            width: 1
                        }),
                        fill: new ol.style.Fill({
                            color: '#ffffa1'  // Cor de preenchimento
                        })
                    })
                ];

                // Adicionando o estilo de texto (nome da sala)
                const name = feature.get('name');
                if (typeof name === 'string' && name.trim() !== '') {
                    const geometry = feature.getGeometry();
                    let labelCoord;

                    // Para polígonos (salas), pegamos o ponto central
                    if (geometry.getType() === 'Polygon' || geometry.getType() === 'MultiPolygon') {
                        labelCoord = geometry.getInteriorPoint().getCoordinates();
                    } else {
                        labelCoord = geometry.getCoordinates(); // Para outros tipos de geometria
                    }

                    // Adicionando o rótulo (nome da sala) ao estilo
                    styles.push(new ol.style.Style({
                        geometry: new ol.geom.Point(labelCoord),
                        text: new ol.style.Text({
                            text: name,
                            font: 'bold 13px sans-serif',  // Fonte do nome
                            fill: new ol.style.Fill({ color: '#000000' }),  // Cor do texto
                            stroke: new ol.style.Stroke({ color: '#ffffff', width: 3 }),  // Contorno do texto
                         
                        }),
                        zIndex: 1000  // Garantir que o nome fique por cima dos outros elementos
                    }));
                }

                // Retorna o array com os dois estilos aplicados (geometria e texto)
                return styles;
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
    map = new ol.Map({
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


    const onChangeOnce = () => {
        if (vectorSource.getState() === 'ready' && !vectorSource.isEmpty()) {
            map.getView().fit(vectorSource.getExtent(), {
                padding: [50, 50, 50, 50],
                maxZoom: 21,
                duration: 500
            });

            vectorSource.un('change', onChangeOnce); // Desativa o listener
        }
    };

    vectorSource.on('change', onChangeOnce);

    // Listener para o seletor de andar
    levelSelector = document.getElementById('level');
    if (levelSelector) {
        levelSelector.value = '1';
        levelSelector.addEventListener('change', function (e) {
            nivelSelecionado = e.target.value;
            vectorLayer.changed(); // força redesenho
            updateFeatureVisibility();
        });
    }

    caminhosSource = new ol.source.Vector();
    caminhosLayer = new ol.layer.Vector({
        source: caminhosSource,
        style: function (feature) {
            const level = feature.get('level');
            const levelList = level ? level.split(';') : [];
            if (!levelList.includes(nivelSelecionado)) return null;

            return new ol.style.Style({
                    stroke: new ol.style.Stroke({
                        color: 'blue',
                        width: 3
                    })
                })
    }});

    map.addLayer(caminhosLayer);

    enableClick();

    map.on('movestart', () => {
        hideContextMenu();
    });


    console.log('Mapa inicializado com sucesso!');
}

function enableClick() {
    if (!mapClickListener) {
        mapClickListener = function (event) {
            clickCoordinate = event.coordinate;
            coordinate = ol.proj.toLonLat(clickCoordinate);

            const pixel = map.getEventPixel(event.originalEvent);
            const mapElement = document.getElementById('map');
            const menu = document.getElementById('context-menu');

            // Calcula posição dentro do mapa
            const rect = mapElement.getBoundingClientRect();
            const x = pixel[0];
            const y = pixel[1];

            // Mostra o menu
            menu.style.left = `${x}px`;
            menu.style.top = `${y}px`;
            menu.style.display = 'block';
        };
        map.on('click', mapClickListener);
    }
}

function disableClick() {
    if (mapClickListener) {
        map.un('click', mapClickListener);
        mapClickListener = null;
    }
}

function updateFeatureVisibility() {
    vectorSource.getFeatures().forEach(feature => {
        const level = feature.get("level");
        const visible = (level === levelSelector.value);
        feature.setStyle(visible ? feature.get("originalStyle") : null);
    });

    caminhosSource.getFeatures().forEach(feature => {
        const level = feature.get('level');
        const visible = (level === levelSelector.value);

        // Exibe ou oculta o segmento com base no nível
        feature.setStyle(visible ? new ol.style.Style({
            stroke: new ol.style.Stroke({
                color: 'blue',
                width: 3
            })
        }) : null);
    });
}

function hideContextMenu() {
    const menu = document.getElementById('context-menu');
    menu.style.display = 'none';
}


window.createPath = () => {
    const nome = prompt("Nome do caminho:");
    if (!nome) return;

    const draw = new ol.interaction.Draw({
        source: caminhosSource,
        type: 'LineString'
    });

    map.addInteraction(draw);
    hideContextMenu();
    disableClick();

    draw.on('drawend', function (e) {
        const feature = e.feature;
        feature.set('name', nome);
        feature.set('level', levelSelector.value); // Associa o caminho ao nível selecionado no dropdown
        map.removeInteraction(draw);
        enableClick();

        // Exibe uma mensagem confirmando o caminho desenhado
        alert(`Caminho para o nível ${levelSelector.value} desenhado com sucesso!`);
        updateFeatureVisibility();
    });
};

function mapCoordinates() {
    return coordinate;
}

function getCurrentLevel() {
    return parseInt(levelSelector.value);
}

function addBeacon(longitude, latitude, level, name = "") {
    if (longitude == null || latitude == null) return;

    const beaconLocation = ol.proj.fromLonLat([longitude, latitude]);

    // Estilo com ícone e texto acima
    const iconWithLabelStyle = new ol.style.Style({
        image: new ol.style.Icon({
            src: 'https://openlayers.org/en/v6.9.0/examples/data/icon.png',
            scale: 1,
            anchor: [0.5, 1]
        }),
        text: new ol.style.Text({
            text: name,
            offsetY: -25,
            scale: 1.2,
            fill: new ol.style.Fill({ color: '#000' }),
            stroke: new ol.style.Stroke({ color: '#fff', width: 2 }),
            textAlign: 'center'
        })
    });

    const beaconFeature = new ol.Feature({
        geometry: new ol.geom.Point(beaconLocation),
        level: level.toString(),
        name: name
    });

    beaconFeature.set("originalStyle", iconWithLabelStyle);
    beaconFeature.setStyle(iconWithLabelStyle);

    vectorSource.addFeature(beaconFeature);

    updateFeatureVisibility();
    hideContextMenu();
}

