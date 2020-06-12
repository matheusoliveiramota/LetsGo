import 'dart:async';
import 'package:flutter/material.dart';
import 'package:fluttergooglemapsapp/models/mesa.dart';
import 'package:fluttergooglemapsapp/models/restaurante.dart';
import 'package:fluttergooglemapsapp/services/restauranteService.dart';

/// This Widget is the main application widget.
class RestauranteUI extends StatefulWidget {
  
  final Restaurante restaurante;

  RestauranteUI({this.restaurante});

  @override
  State<StatefulWidget> createState() {
    
    return ListaMesasState();
  }
}

class ListaMesasState extends State<RestauranteUI> {

  final GlobalKey<ScaffoldState> scaffoldKey = GlobalKey<ScaffoldState>();
  final RestauranteService restauranteService = new RestauranteService();

  DateTime dataUltimaAlteracaoMesa;
  Timer timer;

  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.restaurante.nome),
        actions: <Widget>[
          IconButton(
            icon: const Icon(Icons.help_outline),
            tooltip: 'Situação das Mesas',
            padding: const EdgeInsets.only(right: 22.0),
            onPressed: () {
              showDialog(
                context: context,
                builder: (_) => new AlertDialog(
                    title: new Text("Situação das Mesas"),
                    content: Column(
                              mainAxisSize: MainAxisSize.min,
                              children: <Widget>[
                                Container(
                                  child: Row(
                                    children: <Widget>[
                                      Container(
                                        child: _bulletDetails(Colors.yellow)),
                                      SizedBox(width: 10.0),
                                      Container(
                                        child: Text(
                                          'Mesa não monitorada',
                                          style: TextStyle(
                                            fontSize: 18.0,
                                            color: Colors.black,
                                          ),
                                        ),
                                      )
                                    ],
                                  ),
                                ),
                                SizedBox(height: 12.0),
                                Container(
                                  child: Row(
                                    children: <Widget>[
                                      Container(
                                        child: _bulletDetails(Colors.red)),
                                      SizedBox(width: 10.0),
                                      Container(
                                        child: Text(
                                          'Mesa ocupada',
                                          style: TextStyle(
                                            fontSize: 18.0,
                                            color: Colors.black,
                                          ),
                                        ),
                                      )
                                    ],
                                  ),
                                ),
                                SizedBox(height: 12.0),
                                Container(
                                  child: Row(
                                    children: <Widget>[
                                      Container(
                                        child: _bulletDetails(Colors.green)),
                                      SizedBox(width: 10.0),
                                      Container(
                                        child: Text(
                                          'Mesa livre',
                                          style: TextStyle(
                                            fontSize: 18.0,
                                            color: Colors.black,
                                          ),
                                        ),
                                      )
                                    ],
                                  ),
                                )
                              ],
                            )
                )
              );
            },
          ),
        ],
      ),
      body: CustomScrollView(
        slivers: <Widget>[
          SliverList(
              delegate: SliverChildListDelegate(
                [
                  Container(
                    height: 150.0,
                    decoration: BoxDecoration(
                    image: new DecorationImage(
                        fit: BoxFit.cover,
                        image: NetworkImage(restauranteService.getUrlImagem() + widget.restaurante.urlImagem)
                      ),
                    ),  
                  ),
                  Container(
                    height: 40.0,
                    child: Center (
                      child: Text(
                        widget.restaurante.nome,
                        style: TextStyle(
                          fontWeight: FontWeight.bold,
                          fontSize: 20.0,
                          color: Colors.black,
                        ),
                      )
                    )
                  ),
                  Padding(
                    padding: const EdgeInsets.only(right:15.0, left: 15.0),
                    child: Container(
                      child: Center (
                        child: Text(
                          widget.restaurante.endereco.formatar(),
                          style: TextStyle(
                            fontSize: 18.0,
                            color: Colors.black87,
                          ),
                        )
                      )
                    ),
                  ),
                  widget.restaurante.distanciaRestaurante != null ? 
                  Padding(
                      padding: const EdgeInsets.only(top: 5.0),
                      child: Container(
                        child: Center (
                          child: Text(
                            widget.restaurante.distanciaRestaurante.distancia,
                            style: TextStyle(
                              fontSize: 18.0,
                              color: Colors.amber[900],
                              fontWeight: FontWeight.bold 
                            ),
                          )
                        )
                      ),
                  ) : Text(""),
                  Padding(
                    padding: const EdgeInsets.only(top: 5.0),
                    child: Container(
                      child: Center (
                        child: Text(
                          restauranteService.getTotalMesasDisponiveis(widget.restaurante).toString() + " mesas disponíveis",
                          style: TextStyle(
                            fontWeight: FontWeight.bold,
                            fontSize: 19.0,
                            color: Colors.black87,
                          ),
                        )
                      )
                    ),
                  )
                ],
              ),
          ),
          SliverGrid.count(
            children: <Widget>[CustomScrollView(
                  primary: false,
                  slivers: <Widget>[
                    SliverPadding(
                      padding: const EdgeInsets.only(right:30,left:30,top: 15,bottom: 40),
                      sliver: SliverGrid.count(
                        crossAxisSpacing: 20,
                        mainAxisSpacing: 20,
                        crossAxisCount: 2,
                        children: _buildMesas(),
                      ),
                    ),
                  ],
                )
            ],
            crossAxisCount: 1,
          ),
        ],
      ),
    );
  }

  Widget _bulletDetails(Color color) {
    return Container(
      height: 10.0,
      width: 10.0,
      decoration: new BoxDecoration(
        color: color,
        shape: BoxShape.rectangle
      ),
    );
  }

  List<Widget> _buildMesas() {
    
    var mesas = widget.restaurante.mesas;
    var mesasWidget = new List<Widget>();
    
    for(var i = 0; i < mesas.length; i++){

      var corMesa = Colors.yellow; // Não monitorada
      if(mesas[i].estado == 1) // Livre
        corMesa = Colors.green;
      if(mesas[i].estado == 2) // Ocupada
        corMesa = Colors.red;

      mesasWidget.add(new Container(
                        padding: const EdgeInsets.all(8),
                        child: Center(child: 
                                Text(mesas[i].numero.toString(),
                                style: TextStyle(
                                  fontSize: 25.0,
                                  color: Colors.black
                         ))),
                        color: corMesa
                      )
      );
    }
    
    this.dataUltimaAlteracaoMesa = _getUltimaAlteracaoEstadoMesa(mesas);
    _setTimer();

    return mesasWidget;
  }

  void _setTimer() {

    this.timer = Timer.periodic(
      Duration(seconds: 5),
      (timer){ 

        this.restauranteService.getMesas(widget.restaurante.codRestaurante)
                    .then((mesas) {
        
                      debugPrint('Busca:' + widget.restaurante.nome);
                      if(this.dataUltimaAlteracaoMesa.isBefore(_getUltimaAlteracaoEstadoMesa(mesas))) {
                        setState(() {
                          widget.restaurante.mesas = mesas;
                          timer.cancel();
                        });
                      }
                    }); 
      }
    );
  }

  DateTime _getUltimaAlteracaoEstadoMesa(List<Mesa> mesas) {

    DateTime dataUltimaAlteracaoEstadoMesa = mesas[0].dataAlteracaoEstado;

    for(var mesa in mesas) {

      if(dataUltimaAlteracaoEstadoMesa.isBefore(mesa.dataAlteracaoEstado)) {
        dataUltimaAlteracaoEstadoMesa = mesa.dataAlteracaoEstado;
      }
    }

    return dataUltimaAlteracaoEstadoMesa;
  }

  @override
  void dispose() {
    
    if(timer != null && timer.isActive) {

      timer.cancel();
      super.dispose();
    }
  }
}