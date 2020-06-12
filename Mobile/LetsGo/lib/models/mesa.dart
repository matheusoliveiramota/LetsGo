class Mesa {
  
  int codMesa;
  int numero;
  int estado;
  DateTime dataAlteracaoEstado; 

  Mesa({this.codMesa, this.numero, this.estado, this.dataAlteracaoEstado});
  
  factory Mesa.fromJson(Map<String, dynamic> json) {
    return Mesa(
      codMesa: json['codMesa'],
      numero: json['numero'],
      estado: json['estado'],
      dataAlteracaoEstado: DateTime.parse(json['dataAlteracaoEstado'])
    );
  }
}