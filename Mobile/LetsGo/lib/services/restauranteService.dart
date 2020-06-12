import 'package:dio/dio.dart';
import 'package:fluttergooglemapsapp/models/distanciaRestaurante.dart';
import 'package:fluttergooglemapsapp/models/endereco.dart';
import 'package:fluttergooglemapsapp/models/mesa.dart';
import 'package:fluttergooglemapsapp/models/responseBase.dart';
import 'package:fluttergooglemapsapp/models/restaurante.dart';
import 'package:fluttergooglemapsapp/services/geolocatorService.dart';
import 'package:geolocator/geolocator.dart';

class RestauranteService {

 final url = "http://192.168.15.254:6002/api"; 
 final Dio dio = Dio();

    Future<Restaurante> getRestaurante(int codRestaurante) async {
      
      Response response = await dio.get(url + "/Restaurante/" + codRestaurante.toString());

      if (response.statusCode == 200) {

        Restaurante restaurante = new Restaurante();
        Endereco endereco = new Endereco();
        List<Mesa> mesas = new List<Mesa>();
        
        restaurante = Restaurante.fromJson(response.data);
        endereco = Endereco.fromJson(response.data['endereco']);
        for (Map<String, dynamic> item in response.data['mesas']) {

          mesas.add(Mesa.fromJson(item));
        }

        restaurante.endereco = endereco;
        restaurante.mesas = mesas;

        return restaurante;
      }   
      else {
        throw Exception('Falha ao buscar restaurante');
      }
    }

    Future<ResponseBase<List<Restaurante>>> getRestaurantes() async {

      var responseBase = new ResponseBase<List<Restaurante>>();

      Response response;    
      Position position = await new GeolocatorService().getPosition();
      
      if(position != null) {
        var latitude = position.latitude.toString();
        var longitude = position.longitude.toString();

        response = await dio.get(url + "/Restaurante/Localizacao?latitude=" + latitude +
                                                                 "&longitude=" + longitude);
      }
      else {
        response = await dio.get(url + "/Restaurante/Localizacao");
      }

      List<Restaurante> restaurantes = new List<Restaurante>();

      if (response.statusCode == 200) {
        
        responseBase = ResponseBase.fromJson(response.data);

        for (Map<String, dynamic> restauranteJson in response.data['data']) {

          Restaurante restaurante = new Restaurante();
          Endereco endereco = new Endereco();
          DistanciaRestaurante distanciaRestaurante = new DistanciaRestaurante();
          List<Mesa> mesas = new List<Mesa>();
          
          restaurante = Restaurante.fromJson(restauranteJson);
          endereco = Endereco.fromJson(restauranteJson['endereco']);
          
          for (Map<String, dynamic> mesaJson in restauranteJson['mesas']) {

            mesas.add(Mesa.fromJson(mesaJson));
          }

          if(restauranteJson['distanciaRestaurante'] != null) {
            distanciaRestaurante = DistanciaRestaurante.fromJson(restauranteJson['distanciaRestaurante']);
            restaurante.distanciaRestaurante = distanciaRestaurante;
          }

          restaurante.endereco = endereco;
          restaurante.mesas = mesas;

          restaurantes.add(restaurante);
        }
        
        responseBase.data = restaurantes;

        return responseBase;
      }   
      else {
        throw Exception('Falha ao buscar restaurante');
      }
    }

    Future<ResponseBase<List<Restaurante>>> pesquisaRestaurantes(String busca) async {

      var responseBase = new ResponseBase<List<Restaurante>>();

      Response response;    
      Position position = await new GeolocatorService().getPosition();
      
      if(position != null) {
        var latitude = position.latitude.toString();
        var longitude = position.longitude.toString();

        response = await dio.get(url + "/Restaurante/Pesquisa?latitude=" + latitude +
                                                                 "&longitude=" + longitude +
                                                                 "&busca=" + busca);
      }
      else {
        response = await dio.get(url + "/Restaurante/Pesquisa?busca=" + busca);
      }

      List<Restaurante> restaurantes = new List<Restaurante>();

      if (response.statusCode == 200) {
        
        responseBase = ResponseBase.fromJson(response.data);

        for (Map<String, dynamic> restauranteJson in response.data['data']) {
          

          Restaurante restaurante = new Restaurante();
          Endereco endereco = new Endereco();
          DistanciaRestaurante distanciaRestaurante = new DistanciaRestaurante();
          List<Mesa> mesas = new List<Mesa>();
          
          restaurante = Restaurante.fromJson(restauranteJson);
          endereco = Endereco.fromJson(restauranteJson['endereco']);

          for (Map<String, dynamic> mesaJson in restauranteJson['mesas']) {

            mesas.add(Mesa.fromJson(mesaJson));
          }

          if(restauranteJson['distanciaRestaurante'] != null) {
            distanciaRestaurante = DistanciaRestaurante.fromJson(restauranteJson['distanciaRestaurante']);
            restaurante.distanciaRestaurante = distanciaRestaurante;
          }

          restaurante.endereco = endereco;
          restaurante.mesas = mesas;

          restaurantes.add(restaurante);
        }
        
        responseBase.data = restaurantes;

        return responseBase;
      }   
      else {
        throw Exception('Falha ao buscar restaurante');
      }
    }

    Future<List<Mesa>> getMesas(int codRestaurante) async {
    
    Response response = await dio.get(url + "/Restaurante/GetMesas/" + codRestaurante.toString());

    if (response.statusCode == 200) {

      List<Mesa> mesas = new List<Mesa>();
      
      for (Map<String, dynamic> item in response.data) {

        mesas.add(Mesa.fromJson(item));
      }

      return mesas;
    }   
    else {
      throw Exception('Falha ao buscar mesas');
    }
  }

  int getTotalMesasDisponiveis(Restaurante restaurante) {

      return restaurante.mesas.where((mesa) => mesa.estado == 1).length;
  }

  String getUrlImagem() {

    return this.url + "/Restaurante/Imagem/";
  }
}