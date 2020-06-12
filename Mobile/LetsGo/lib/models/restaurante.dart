import 'package:fluttergooglemapsapp/models/distanciaRestaurante.dart';

import 'endereco.dart';
import 'mesa.dart';

class Restaurante {

  int codRestaurante;
  String nome;
  String urlImagem;
  Endereco endereco;
  List<Mesa> mesas;
  DistanciaRestaurante distanciaRestaurante;

  Restaurante({this.codRestaurante, this.nome, this.endereco, this.mesas, this.urlImagem});

  factory Restaurante.fromJson(Map<String, dynamic> json) {
    return Restaurante(
      codRestaurante: json['codRestaurante'],
      nome: json['nome'],
      urlImagem: json['nomeImagem']
    );
  }
}