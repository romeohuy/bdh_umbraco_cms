function initMap() {

    // Create a new StyledMapType object, passing it an array of styles,
    // and the name to be displayed on the map type control.
    var styledMapType = new google.maps.StyledMapType(
        [
            {
                "featureType": "all",
                "elementType": "all",
                "stylers": [
                    {
                        "saturation": -100
                    },
                    {
                        "gamma": 0.5
                    }
                ]
            }
            ,
            {
                "elementType": "labels.icon",
                "stylers": [
                    {
                        "visibility": "off"
                    }
                ]
            }
            ,
            {
                "featureType": "poi.school",
                "stylers": [
                    {
                        "visibility": "on"
                    }
                ]
            },
        ],
        {name: 'bdh.com.vn'});

    // Create a map object, and include the MapTypeId to add
    // to the map type control.,,
    var map = new google.maps.Map(document.getElementById('map'), {
        center: {lat: 10.769533, lng: 106.696707},
        zoom: 17,
        mapTypeControlOptions: {
            mapTypeIds: ['roadmap', 'satellite', 'hybrid', 'terrain',
                'styled_map']
        },
        zoomControl: false,
        mapTypeControl: false,
        scaleControl: false,
        streetViewControl: false,
        rotateControl: false,
        fullscreenControl: false,
        gestureHandling: "cooperative"
    });
    var marker = new google.maps.Marker({
        position: {lat: 10.769635, lng: 106.700140},
        map: map,
        title: 'bdh.com.vn'
    });

    var infowindowContent = "" +
        "<img src='/images/header-logo.png' style='height:40px;width: auto' alt='BDH Logo'>";
    var infowindow = new google.maps.InfoWindow({
        content: infowindowContent
    });
    var infoContent = "";
    infowindow.open(map, marker);
    //Associate the styled map with the MapTypeId and set it to display.
    map.mapTypes.set('styled_map', styledMapType);
    map.setMapTypeId('styled_map');
}