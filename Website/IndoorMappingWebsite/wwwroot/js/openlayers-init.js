// Função para carregar os scripts
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

    const map = new ol.Map({
        target: 'map', 
        layers: [
            new ol.layer.Tile({
                source: new ol.source.OSM()  
            })
        ],
        view: new ol.View({
            center: ol.proj.fromLonLat([-8.607736752623799,41.1777948256557 ]), 
            zoom: 19
        })
    });

    console.log('Mapa inicializado com sucesso!');
}