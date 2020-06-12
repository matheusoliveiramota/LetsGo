import 'dart:async';
import 'package:flutter/material.dart';
import 'package:fluttergooglemapsapp/models/responseBase.dart';
import 'package:fluttergooglemapsapp/models/restaurante.dart';
import 'package:fluttergooglemapsapp/screens/restauranteUI.dart';
import 'package:fluttergooglemapsapp/services/restauranteService.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';

class HomePage extends StatefulWidget {

  @override
  HomePageState createState() => HomePageState();
}

class HomePageState extends State<HomePage> {

  final TextEditingController _pesquisaController = new TextEditingController();
  Completer<GoogleMapController> _controller = Completer();
  List<Restaurante> listaRestaurantes = new List<Restaurante>();

  RestauranteService restauranteService = new RestauranteService();

  @override
  void initState() {
    super.initState();
  }
  
  @override
  Widget build(BuildContext context) {

    Future<ResponseBase<List<Restaurante>>> responseBase;
    
    if(_pesquisaController.text != null && _pesquisaController.text.isNotEmpty) {
      responseBase = restauranteService.pesquisaRestaurantes(_pesquisaController.text);
    }
    else {
      responseBase = restauranteService.getRestaurantes();
    }

    return Scaffold(
      appBar: AppBar(
        title: TextField(
            controller: _pesquisaController,
            obscureText: false,
            decoration: InputDecoration(
              labelText: 'Pesquisar',
              labelStyle: TextStyle(
                color: Colors.white,
                fontStyle: FontStyle.italic
              )
            ),
            style: TextStyle(
              color: Colors.white
            ),
            onSubmitted: (text) => setState((){}),
          ),
        actions: <Widget>[
          IconButton(
              icon: Icon(FontAwesomeIcons.search),
              onPressed: () {
                setState((){});
              }),
        ],
      ),
      body: FutureBuilder<ResponseBase<List<Restaurante>>>(
      future: responseBase,
      builder: (context, response) {

        if (response.hasData) {

          this.listaRestaurantes = response.data.data;

          return Stack(
            children: <Widget>[
              _buildGoogleMap(context),
              _buildContainer(),
              (response.data.message != null && response.data.message.isNotEmpty) 
              ? _showErrorMessage(response.data.message)
              : Text("")
            ],
          );
        } else if (response.hasError) {
          return AlertDialog(
              title: Text("Erro"),
              content: Text("${response.error}"),
              actions: [
                FlatButton(
                  child: Text("OK"),
                  onPressed: () => Navigator.of(context).pop(),
                ),
              ],
            );
        }
        return Center(child:CircularProgressIndicator());
      },
     )
    );
  }

  Widget _buildContainer() {
    
    var itensWidget = List<Widget>();

    for(var restaurante in this.listaRestaurantes) {
      
      itensWidget.add(SizedBox(width: 10.0));

      itensWidget.add(Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: _boxes(
                            restaurante.urlImagem,restaurante),
                      )
      );
    }  

    return Align(
      alignment: Alignment.bottomLeft,
      child: Container(
        margin: EdgeInsets.symmetric(vertical: 20.0),
        height: 150.0,
        child: ListView(
          scrollDirection: Axis.horizontal,
          children: itensWidget,
        ),
      ),
    );
  }

  Widget _boxes(String _image,Restaurante restaurante) {
    return  GestureDetector(
        onTap: () {

          Navigator.push(context, MaterialPageRoute(
              builder: (context) => RestauranteUI(restaurante: restaurante)
          ));
        },
        child:Container(
              child: new FittedBox(
                child: Material(
                    color: Colors.white,
                    elevation: 14.0,
                    borderRadius: BorderRadius.circular(24.0),
                    shadowColor: Color(0x802196F3),
                    child: Row(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      children: <Widget>[
                        Container(
                          width: 180,
                          height: 200,
                          child: ClipRRect(
                            borderRadius: new BorderRadius.circular(24.0),
                            child: Image.network(
                                    restauranteService.getUrlImagem() + _image,
                                  ),
                          ),),
                          Container(
                          child: Padding(
                            padding: const EdgeInsets.all(8.0),
                            child: myDetailsContainer1(restaurante),
                          ),
                        ),

                      ],)
                ),
              ),
            ),
    );
  }

  Widget myDetailsContainer1(Restaurante restaurante) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.spaceEvenly,
      children: <Widget>[
        Padding(
          padding: const EdgeInsets.only(left: 8.0),
          child: Container(
              child: Text(restaurante.nome,
              style: TextStyle(
                color: Colors.amber[900],
                fontSize: 24.0,
                fontWeight: FontWeight.bold),
          )),
        ),
        SizedBox(height:5.0),
        Container(
          child: Text(
          restauranteService.getTotalMesasDisponiveis(restaurante).toString() + " mesas disponíveis",
          style: TextStyle(
              color: Colors.black,
              fontSize: 18.0,
              fontWeight: FontWeight.bold),
        )),
        (restaurante.distanciaRestaurante != null && restaurante.distanciaRestaurante.distancia.isNotEmpty) ? 
        Padding(
          padding: const EdgeInsets.only(top: 5.0),
          child: Container(
            child: Text(
            restaurante.distanciaRestaurante.distancia,
            style: TextStyle(
                color: Colors.amber[900],
                fontSize: 18.0,
                fontWeight: FontWeight.bold),
          ))) : Text(""),
      ],
    );
  }

  Widget _buildGoogleMap(BuildContext context) {
    
    Set<Marker> markers = new Set<Marker>();
    
    for(var restaurante in this.listaRestaurantes) {
      markers.add(
        Marker(
          markerId: MarkerId(restaurante.codRestaurante.toString()),
          position: LatLng(restaurante.endereco.latitude, restaurante.endereco.longitude),
          infoWindow: 
            InfoWindow(
              title: restaurante.nome,
              snippet: (restaurante.distanciaRestaurante != null && restaurante.distanciaRestaurante.distancia.isNotEmpty) ?
                       restaurante.distanciaRestaurante.distancia :
                       null
            ),
          icon: BitmapDescriptor.defaultMarkerWithHue(
            BitmapDescriptor.hueOrange,
          ),
          onTap: () {
            
            Navigator.push(context, MaterialPageRoute(
                builder: (context) => RestauranteUI(restaurante: restaurante)
            ));
          }
        )
      );
    }

    if(this.listaRestaurantes[0].endereco.latitude != 0.0 && this.listaRestaurantes[0].endereco.longitude != 0.0) {
      _gotoLocation(this.listaRestaurantes[0].endereco.latitude,this.listaRestaurantes[0].endereco.longitude);
    }

    return Container(
      height: MediaQuery.of(context).size.height,
      width: MediaQuery.of(context).size.width,
      child: GoogleMap(
        mapType: MapType.normal,
        initialCameraPosition:  
          CameraPosition(
            target: LatLng(this.listaRestaurantes[0].endereco.latitude,this.listaRestaurantes[0].endereco.longitude), 
            zoom: 13
          ),
        onMapCreated: (GoogleMapController controller) {
          _controller.complete(controller);
        },
        markers: markers,
      ),
    );
  }

  Future<void> _gotoLocation(double lat,double long) async {

    final GoogleMapController controller = await _controller.future;
    
    LatLng position = LatLng(lat,long);
    controller.moveCamera(CameraUpdate.newLatLng(position));
  }

  Widget _showErrorMessage(String mensagem) {
   return 
    Container(
      color: Colors.red,//Colors.amber[900],
      height: 32.0,
      width: MediaQuery.of(context).size.width,
      child: 
        Center(
            child:
              Text("Ops! " + mensagem,
                style: TextStyle(
                  color: Colors.white,
                  fontSize: 15.0,
                  fontWeight: FontWeight.bold),
            ),
        ),
      );
  }
}