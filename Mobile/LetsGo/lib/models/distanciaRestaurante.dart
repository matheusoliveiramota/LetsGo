class DistanciaRestaurante {

  String distancia;
  int distanciaEmMetros;

  DistanciaRestaurante({this.distancia, this.distanciaEmMetros});

  factory DistanciaRestaurante.fromJson(Map<String, dynamic> json) {
    return DistanciaRestaurante(
      distancia: json['distancia'],
      distanciaEmMetros: json['distanciaEmMetros']
    );
  }
}