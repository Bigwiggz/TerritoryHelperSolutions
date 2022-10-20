/*
Leaflet Javascript File
*/



///////////////////////////////////////////
//Create Map with coordinates
///////////////////////////////////////////
let map = L.map('map').setView([33.982885, -81.236141], 13);

///////////////////////////////////////////
//Create Global Variables
///////////////////////////////////////////

//Used to display variables
let mapTerritoryList = [];
let mapExistingAddressList = [];
let mapNewAddressList = [];


//used to filter list on input
let filteredTerritoryList = [];
let filteredExistingAddressList = [];
let filteredNewAddressList = [];



///////////////////////////////////////////
//Add Map Layers
///////////////////////////////////////////

let osm=L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
})

let googleStreets = L.tileLayer('http://{s}.google.com/vt/lyrs=m&x={x}&y={y}&z={z}',{
    maxZoom: 20,
    subdomains:['mt0','mt1','mt2','mt3']
});

let googleHybrid = L.tileLayer('http://{s}.google.com/vt/lyrs=s,h&x={x}&y={y}&z={z}',{
    maxZoom: 20,
    subdomains:['mt0','mt1','mt2','mt3']
});

let googleSat = L.tileLayer('http://{s}.google.com/vt/lyrs=s&x={x}&y={y}&z={z}',{
    maxZoom: 20,
    subdomains:['mt0','mt1','mt2','mt3']
});

let googleTerrain = L.tileLayer('http://{s}.google.com/vt/lyrs=p&x={x}&y={y}&z={z}',{
    maxZoom: 20,
    subdomains:['mt0','mt1','mt2','mt3']
});

let greyblank= L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/Canvas/World_Light_Gray_Base/MapServer/tile/{z}/{y}/{x}', {
	attribution: 'Tiles &copy; Esri &mdash; Esri, DeLorme, NAVTEQ',
	maxZoom: 16
});

let blockMap= L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}', {
	attribution: 'Tiles &copy; Esri &mdash; Source: Esri, DeLorme, NAVTEQ, USGS, Intermap, iPC, NRCAN, Esri Japan, METI, Esri China (Hong Kong), Esri (Thailand), TomTom, 2012'
});

let EsriWorldStreetMap = L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}', {
	attribution: 'Tiles &copy; Esri &mdash; Source: Esri, DeLorme, NAVTEQ, USGS, Intermap, iPC, NRCAN, Esri Japan, METI, Esri China (Hong Kong), Esri (Thailand), TomTom, 2012'
});

osm.addTo(map);

//import list of locations
filteredTerritoryList = territories;
filteredExistingAddressList = existingAddresses;
filteredNewAddressList = newAddresses;


//Add scale to map
L.control.scale({position: 'bottomright'}).addTo(map);


///////////////////////////////////////////
//Add Existing Address List
///////////////////////////////////////////
let switchIcon=L.icon({
	iconUrl:'./img/existingAddressNBG.png',
	iconSize:[20, 20]
});

for(let i=0;i< filteredExistingAddressList.features.length; i++){
	let singleMarker=L.marker([filteredExistingAddressList.features[i].geometry.coordinates[1],filteredExistingAddressList.features[i].geometry.coordinates[0]],{
		title: `Address: ${filteredExistingAddressList.features[i].properties.FullAddress}`,
		icon: switchIcon
	});
	
	//Add Popup
	let popup = singleMarker.bindPopup(`${filteredExistingAddressList.features[i].properties.FullAddress}`).openPopup();
	
	//Add 
	singleMarker.on("click",function(e){
		LoadItemInformation(filteredExistingAddressList.features[i].properties);
	});

	mapExistingAddressList.push(singleMarker);
};

///////////////////////////////////////////
//Add New Address List
///////////////////////////////////////////
let realSwitchIcon=L.icon({
	iconUrl:'./img/newAddressNBG.png',
	iconSize:[20, 20]
});

for(let i=0;i< filteredNewAddressList.features.length; i++){
	let singleMarker=L.marker([filteredNewAddressList.features[i].geometry.coordinates[1],filteredNewAddressList.features[i].geometry.coordinates[0]],{
		title: `Address: ${filteredNewAddressList.features[i].properties.FullAddress}`,
		icon: realSwitchIcon
	});
	
	//Add Popup
	let popup = singleMarker.bindPopup(`${filteredNewAddressList.features[i].properties.FullAddress}`).openPopup();
	
	//Add 
	singleMarker.on("click",function(e){
		LoadItemInformation(filteredNewAddressList.features[i].properties);
	});

	mapNewAddressList.push(singleMarker);
};




///////////////////////////////////////////
//Add Territories List
///////////////////////////////////////////
let geoAuxParcel = L.geoJSON(filteredTerritoryList,{
	style:function(feature){
		return{
			color: `#${feature.properties.TerritoryTypeColor}`,
			fillColor: `#${feature.properties.TerritoryTypeColor}`,
			fill:true,
			fillOpacity: 0.2
		}
	},
	onEachFeature:function(feature, layer){
		layer.bindTooltip(feature.properties.name,{
			permanent:true,
			direction:"center",
			className:"territoryLabel"
		});
		layer.on('click',function(){
			let polygonProperties=feature.properties;
			LoadItemInformation(polygonProperties);
		});
	}
});





///////////////////////////////////////////
//Create Layer Groups
///////////////////////////////////////////
let mapTerritoryGroup = L.layerGroup(mapTerritoryList);
let mapExistingAddressesGroup = L.layerGroup(mapExistingAddressList);
let mapNewAddressesGroup = L.layerGroup(mapNewAddressList);


///////////////////////////////////////////
//Add Map Layer Controller
///////////////////////////////////////////

//Base Maps to Layer
let baseMaps={
	"OSM": osm,
	"Google Streets": googleStreets,
	"Google Satelite":googleSat,
	"Google Hybrid":googleHybrid,
	"Google Terrain":googleTerrain,
	"Block Map": blockMap,
	"Gray": greyblank,
	"Esri Map":EsriWorldStreetMap,
};

//Shapes to Layer
let overlayMaps = {
	"Territories": geoAuxParcel,
	"Existing Addresses": mapExistingAddressesGroup,
	"New Addresses": mapNewAddressesGroup
};

L.control.layers(baseMaps,overlayMaps).addTo(map);







///////////////////////////////////////////
//Function to add input information
///////////////////////////////////////////

function LoadItemInformation(properties){
	
	
	//Append html table information
	const InformationTable=document.getElementById("InformationTableBody");
	InformationTable.textContent="";

	let htmlTable="";
	
	for (var key of Object.keys(properties)) {
		let columnField=key.replace(/([A-Z])/g, ' $1').trim();
		let columnValue=properties[key];
		
		htmlTable+=
		`
		<tr>
			<td><b>${columnField}</b></td>
			<td>${columnValue}</td>
		</tr>
		`
	}
	
	//Append html table information
	InformationTable.insertAdjacentHTML('afterbegin',htmlTable);

	//turn visibility of element on to display table
	document.getElementById("table-information-section").style.visibility="visible";

	//clear input array after information is posted
	result=[];
	
	//scroll to Item Information Table
	document.getElementById("table-information-section").scrollIntoView();
	
}
 
///////////////////////////////////////////
//Add Leaflet Drawing Controls 
///////////////////////////////////////////

map.on('pm:create', e => {
  generateGeoJson();
  ClearAllDrawingLayers();
});


map.pm.addControls({  
  position: 'topleft',
  editMode: true,
  removalMode: true,
  drawMarker:true,
  drawPolygon:true,
  drawRectangle:false,
  drawCircle:false,
  drawCircleMarker:false
});




/*
Function to get geoJSON after shape has been created

map.on('pm:create',(e) {
  e.layer.on('pm:edit', ({ layer }) => {
    // layer has been edited
    console.log(layer.toGeoJSON());
  })
});
*/

/*
Function to clear all shapes


map.eachLayer(function(layer){
     if (layer._path != null) {
    layer.remove()
  }
});

*/


function ClearAllDrawingLayers(){
	map.eachLayer(function(layer){
     if (layer._path != null) {
    layer.remove()
	  }
	});
};


function generateGeoJson(){
	var fg = L.featureGroup();    
	var layers = findLayers(map);
  layers.forEach(function(layer){
  	fg.addLayer(layer);
  });
	LoadTerritoryJSONText(fg);
	console.log(fg.toGeoJSON());
}

function findLayers(map) {
    var layers = [];
    map.eachLayer(layer => {
      if (
        layer instanceof L.Polyline ||
        layer instanceof L.Marker ||
        layer instanceof L.Circle ||
        layer instanceof L.CircleMarker
      ) {
        layers.push(layer);
      }
    });

    // filter out layers that don't have the leaflet-geoman instance
    layers = layers.filter(layer => !!layer.pm);

    // filter out everything that's leaflet-geoman specific temporary stuff
    layers = layers.filter(layer => !layer._pmTempLayer);

    return layers;
};


