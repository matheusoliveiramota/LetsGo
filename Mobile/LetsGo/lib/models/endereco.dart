class Endereco {

  int codEndereco;
  String uf;
  String cidade;
  String bairro;
  String cep;
  String rua;
  String numero;
  String complemento;
  double latitude;
  double longitude;

  Endereco({this.codEndereco, this.cidade, this.bairro, this.cep, this.rua, this.numero, this.complemento, this.uf, this.latitude, this.longitude});

  factory Endereco.fromJson(Map<String, dynamic> json) {
    return Endereco(
      codEndereco: json['codEndereco'],
      uf: json['estado']['sigla'],
      cidade: json['cidade'],
      bairro: json['bairro'],
      cep: json['cep'],
      rua: json['rua'],
      numero: json['numero'],
      complemento: json['complemento'],
      latitude: json['latitude'].toDouble(),
      longitude: json['longitude'].toDouble()
    );
  }

  String formatar() {
    return this.rua + ", " + 
           this.numero + " " + 
           (this.complemento == null ? "" : this.complemento) + " - " +
           this.bairro + " - " +
           this.cidade + "," + this.uf;
  }
}