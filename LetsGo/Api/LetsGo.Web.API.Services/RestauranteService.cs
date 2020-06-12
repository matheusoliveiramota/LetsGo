using LetsGo.Domain.Entities;
using LetsGo.Domain.Entities.Responses;
using LetsGo.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace LetsGo.Web.API.Services
{
    public class RestauranteService : IRestauranteService
    {
        private readonly string apiKey = "YOUR_API_KEY";
        protected readonly IRestauranteRepository _restauranteRepository;
        protected readonly ILocalizacaoRepository _localizacaoRepository;

        public RestauranteService(IRestauranteRepository repository, ILocalizacaoRepository localizacaoRepository)
        {
            _restauranteRepository = repository;
            _localizacaoRepository = localizacaoRepository;
        }
        
        public List<Restaurante> BuscarRestaurantesProximos(List<Restaurante> restaurantes,double latitude, double longitude)
        {
            List<Restaurante> restaurantesComDistancia = new List<Restaurante>();

            if (latitude != 0 && longitude != 0)
            {
                restaurantesComDistancia = restaurantes.Where(r => r.Endereco.Latitude != 0 && r.Endereco.Longitude != 0).ToList();

                List<string> destinos = restaurantesComDistancia.Select(r => string.Concat(
                                                                                r.Endereco.Latitude.ToString().Replace(",",".")
                                                                                ,","
                                                                                ,r.Endereco.Longitude.ToString().Replace(",",".")
                                                                            )).ToList();

                string destinosFormatado = string.Join("|",destinos);
                string origemFormatado = string.Concat(latitude.ToString().Replace(",",".")
                                                    ,","
                                                    ,longitude.ToString().Replace(",","."));

                var response = ChamadaApiGET("https://maps.googleapis.com",
                                string.Concat("/maps/api/distancematrix/json?units=metric&origins=", origemFormatado,
                                    "&destinations=",destinosFormatado,"&mode=walking&key=",apiKey));

                if (response != null)
                {
                    GetDistanceMatrixResponse distanceMatrix = JsonConvert.DeserializeObject<GetDistanceMatrixResponse>(response);
                    if(distanceMatrix.Status == "OK")
                    {
                        for(int i=0; i < distanceMatrix.Rows[0].Elements.Count ; i++)
                        {
                            if(distanceMatrix.Rows[0].Elements[i].Status == "OK")
                            {
                                restaurantesComDistancia[i].DistanciaRestaurante = new DistanciaRestaurante();
                                restaurantesComDistancia[i].DistanciaRestaurante.Distancia = string.Concat(distanceMatrix.Rows[0].Elements[i].Distance.Text, " de distância");
                                restaurantesComDistancia[i].DistanciaRestaurante.DistanciaEmMetros = distanceMatrix.Rows[0].Elements[i].Distance.Value;
                            }
                        }
                    }
                }
            }

            var restaurantesSemDistancia = restaurantes.Where(rest => !restaurantesComDistancia.Select(restLatLng => restLatLng.CodRestaurante)
                                                                                            .Contains(rest.CodRestaurante))
                                                                                            .ToList();

            restaurantes = restaurantesComDistancia.OrderBy(resp => resp.DistanciaRestaurante.DistanciaEmMetros).ToList();
            restaurantes.AddRange(restaurantesSemDistancia);  

            return restaurantes;
        }

        public void BuscarLatitudeLongitude(Endereco endereco)
        {
            GetGeoPointGoogleResponse result;
            
            var estado = _localizacaoRepository.BuscarEstados().Where(e => e.CodEstado == endereco.Estado.CodEstado).First();
            
            string enderecoFormatado = string.Concat(endereco.Rua, ",", endereco.Numero, "-", endereco.Bairro, ",", endereco.Cidade, "-", estado.Sigla)
                                        .Replace(" ","+");

            var response = ChamadaApiGET("https://maps.googleapis.com",
                                       string.Concat("/maps/api/geocode/json?address=", enderecoFormatado,"&key=", apiKey));

            if (response != null)
            {
                result = JsonConvert.DeserializeObject<GetGeoPointGoogleResponse>(response);
                if (result.Results != null && result.Results.Count > 0)
                {
                    endereco.Latitude = result.Results[0].geometry.location.lat; 
                    endereco.Longitude = result.Results[0].geometry.location.lng;
                }
            }
        }

        private dynamic ChamadaApiGET(string baseAdress,string strApiAddress)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAdress);

                    HttpResponseMessage result = client.GetAsync(strApiAddress).GetAwaiter().GetResult();

                    if (result.IsSuccessStatusCode)
                    {
                        using (HttpContent respostahttp = result.Content)
                        {
                            return respostahttp.ReadAsStringAsync().GetAwaiter().GetResult();
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
