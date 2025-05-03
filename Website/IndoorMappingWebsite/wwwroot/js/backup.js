function addBeacon(longitude, latitude) {
    // Cria o ícone do beacon
    const beaconIcon = new ol.style.Icon({
        src: 'https://openlayers.org/en/v6.9.0/examples/data/icon.png',
        scale: 0.1
    });

    const beaconLocation = ol.proj.fromLonLat([longitude, latitude]);
    const beaconFeature = new ol.Feature({
        geometry: new ol.geom.Point(beaconLocation)
    });

    beaconFeature.setStyle(new ol.style.Style({
        image: beaconIcon
    }));

    const vectorSource = new ol.source.Vector({
        features: [beaconFeature]
    });

    if (vectorLayer) {
        map.removeLayer(vectorLayer);
    }

    vectorLayer = new ol.layer.Vector({
        source: vectorSource
    });

    map.addLayer(vectorLayer);
}

function addPath(longitude, latitude) {
    // Adiciona um ponto ao caminho
    const coordinate = ol.proj.fromLonLat([longitude, latitude]);
    clickCoordinates.push(coordinate);

    const lineGeometry = new ol.geom.LineString(clickCoordinates);
    const lineFeature = new ol.Feature({
        geometry: lineGeometry
    });

    lineFeature.setStyle(new ol.style.Style({
        stroke: new ol.style.Stroke({
            color: 'blue',
            width: 4
        })
    }));

    const vectorSource = new ol.source.Vector({
        features: [lineFeature]
    });

    if (vectorLayer) {
        map.removeLayer(vectorLayer);
    }

    vectorLayer = new ol.layer.Vector({
        source: vectorSource
    });

    map.addLayer(vectorLayer);
}
function hideContextMenu() {
    const menu = document.getElementById('context-menu');
    menu.style.display = 'none';
}

window.createBeacon = () => {
    if (!clickCoordinate) return;

    const iconStyle = new ol.style.Style({
        image: new ol.style.Icon({
            src: 'https://openlayers.org/en/v6.9.0/examples/data/icon.png', // Troca aqui se quiser outra
            scale: 1,
            anchor: [0.5, 1]
        })
    });

    const feature = new ol.Feature({
        geometry: new ol.geom.Point(clickCoordinate),
        level: levelSelector.value
    });

    feature.set("originalStyle", iconStyle);
    feature.setStyle(iconStyle);

    vectorSource.addFeature(feature);

    updateFeatureVisibility();
    hideContextMenu();
};

window.createPath = () => {
    alert("Função para criar caminho ainda não implementada.");
    hideContextMenu();
};



window.createBeacon = () => {
    if (!clickCoordinate) return;

    const iconStyle = new ol.style.Style({
        image: new ol.style.Icon({
            src: 'https://openlayers.org/en/v6.9.0/examples/data/icon.png', // Troca aqui se quiser outra
            scale: 1,
            anchor: [0.5, 1]
        })
    });

    const feature = new ol.Feature({
        geometry: new ol.geom.Point(clickCoordinate),
        level: levelSelector.value
    });

    feature.set("originalStyle", iconStyle);
    feature.setStyle(iconStyle);

    vectorSource.addFeature(feature);

    updateFeatureVisibility();
    hideContextMenu();
};