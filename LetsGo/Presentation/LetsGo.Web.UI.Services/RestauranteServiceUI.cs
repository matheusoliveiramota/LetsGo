using LetsGo.Domain.Entities;
using LetsGo.Web.UI.Models;
using LetsGo.Web.UI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LetsGo.Web.UI.Services
{
    public class RestauranteServiceUI : ServiceUI, IRestauranteServiceUI
    {
        public RestauranteServiceUI(IConfiguration configuration) : base(configuration) { }

        public bool ExisteAlteracao(int codRestaurante, DateTime dataUltimaAlteracaoClient)
        {
            DateTime dataUltimaAlteracaoServer = DateTime.MinValue;

            var response = ChamadaApiGET("/api/Restaurante/GetUltimaAlteracoMesa/" + codRestaurante);

            if (response != null)
            {
                dataUltimaAlteracaoServer = JsonConvert.DeserializeObject<DateTime>(response);
            }

            return dataUltimaAlteracaoServer > dataUltimaAlteracaoClient;
        }

        public Restaurante GetByUsuario(Usuario usuario)
        {
            Restaurante restaurante = null;

            var response = ChamadaApiGET("/api/Restaurante/" + usuario.NomeDeUsuario);

            if (response != null)
            {
                restaurante = JsonConvert.DeserializeObject<Restaurante>(response);
            }

            return restaurante;
        }

        public IEnumerable<Mesa> GetMesas(int codRestaurante)
        {
            IEnumerable<Mesa> mesas = null;

            var response = ChamadaApiGET("/api/Restaurante/GetMesas/" + codRestaurante);

            if (response != null)
            {
                mesas = JsonConvert.DeserializeObject<IEnumerable<Mesa>>(response);
            }

            return mesas;
        }

        public Restaurante InsertRestaurante(RestauranteUI restauranteUI)
        {
            List<Mesa> mesas = new List<Mesa>();
            
            for(int i=0;i < restauranteUI.Pinos.Count; i++)
            {
                Mesa mesa = new Mesa
                {
                    Numero = i + 1,
                    Pino = new Pino
                    {
                        CodPino = restauranteUI.Pinos[i]
                    }
                };

                mesas.Add(mesa);
            }

            mesas = CalcularCoordenadas(mesas);

            Restaurante restaurante = new Restaurante {
                Nome = restauranteUI.Nome,
                Usuario = new Usuario
                {
                    CodUsuario = Convert.ToInt32(restauranteUI.CodUsuario)
                },
                Endereco = new Endereco
                {
                    Estado = new Estado {
                        CodEstado = restauranteUI.CodEstado
                    },
                    Cidade = restauranteUI.Cidade,
                    Cep = restauranteUI.Cep,
                    Rua = restauranteUI.Endereco,
                    Bairro = restauranteUI.Bairro,
                    Numero = restauranteUI.Numero,
                    Complemento = restauranteUI.Complemento
                },
                NomeImagem = SalvarImagen(restauranteUI.Imagem),
                Mesas = mesas,
                ItemPlaca = new ItemPlaca { Placa = new Placa { CodPlaca = restauranteUI.CodPlaca } }
            };

            string serializedRestaurante = JsonConvert.SerializeObject(restaurante);

            var response = ChamadaApiPost(serializedRestaurante, "/api/Restaurante");

            return restaurante;
        }

        private List<Mesa> CalcularCoordenadas(List<Mesa> mesas)
        {
            const int larguraTotal = 812;
            const int comprimentoTopo = 30;

            int qtdMesas = mesas.Count;

            if (qtdMesas > 0)
            {
                if (qtdMesas == 1)
                {
                    mesas = InserirCoordenadas(mesas, larguraTotal, comprimentoTopo, 1, 200);
                }
                else if (qtdMesas >= 2 && qtdMesas <= 6)
                {
                    mesas = InserirCoordenadas(mesas, larguraTotal, comprimentoTopo, 2, 200);
                }
                else if (qtdMesas >= 7 && qtdMesas <= 16)
                {
                    mesas = InserirCoordenadas(mesas, larguraTotal, comprimentoTopo, 4, 150);
                }
                else
                {
                    mesas = InserirCoordenadas(mesas, larguraTotal, comprimentoTopo, 8, 60);
                }
            }

            return mesas;
        }

        private List<Mesa> InserirCoordenadas(List<Mesa> mesas, int larguraTotal, int comprimentoTopo, int qtdMesasPorLinha, int comprimentoMesa)
        {
            int larguraEntreMesas = (larguraTotal - (comprimentoMesa * qtdMesasPorLinha)) / (qtdMesasPorLinha + 1);
            int larguraLinha = larguraEntreMesas;
            int espacoAltura = comprimentoTopo;

            int qtdMesas = 1; 

            for (int i = 0; i < mesas.Count; i++)
            {
                if(qtdMesas > qtdMesasPorLinha)
                {
                    larguraLinha = larguraEntreMesas;
                    espacoAltura += comprimentoMesa + comprimentoTopo;
                    qtdMesas = 1;
                }
                
                mesas[i].Coordenada = new Coordenada { Topo = espacoAltura, Esquerda = larguraLinha, Altura = comprimentoMesa, Largura = comprimentoMesa };
                larguraLinha += larguraEntreMesas + comprimentoMesa;

                qtdMesas++;
            }

            return mesas;
        }

        private string SalvarImagen(IFormFile file) 
        {
            var tiposSuportados = new[] {".jpg", ".jpeg", ".png", ".JPG", ".JPEG", ".PNG"};
            var fileExt = Path.GetExtension(file.FileName);
            
            if (!tiposSuportados.Contains(fileExt))
            {
                return string.Empty;
            }

            var uploadPath = this._configuration.GetSection("UploadPathImagens").Value;
            var fileName = System.Guid.NewGuid().ToString() + fileExt;
                
            using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create))
            {
                try
                {
                    file.CopyTo(fileStream);
                }
                catch(Exception)
                {
                    return string.Empty;
                }
            }

            return fileName;
        }
    }
}
