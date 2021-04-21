using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Controle_de_estoque.Models
{
    public class UnidadeMedidaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Preencha o campo nome.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Preencha o campo Sigla.")]
        public string Sigla { get; set; }
        public bool Ativo { get; set; }

        public static int RecuperarQuant()
        {
            var ret = 0;

            //criar objeto conexão
            using (var conexao = new SqlConnection())
            {
                //dados da conexão
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;

                //abrir conexão
                conexao.Open();

                using (var comando = new SqlCommand())
                {

                    //conectar comando com a conexao no banco
                    comando.Connection = conexao;

                    //comando
                    comando.CommandText = "select count(*) from unidade_medida";


                    ret = (int)comando.ExecuteScalar();
                }
            }
            return ret;
        }

        public static List<UnidadeMedidaModel> RecuperarLista(int pag, int tamPag)
        {
            var ret = new List<UnidadeMedidaModel>();

            //criar objeto conexão
            using (var conexao = new SqlConnection())
            {
                //dados da conexão
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;

                //abrir conexão
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    var pos = (pag - 1) * tamPag;

                    //conectar comando com a conexao no banco
                    comando.Connection = conexao;

                    //comando]
                    //offset = apartir de qual registro devo retornar
                    //rows fetch next  = quantos registros devo retornar
                    comando.CommandText = "select * from unidade_medida order by Nome " +
                        "offset @pos rows fetch next @pag rows only;";

                    comando.Parameters.Add("@pos", SqlDbType.Int).Value = pos > 0 ? pos - 1 : 0;
                    comando.Parameters.Add("@pag", SqlDbType.Int).Value = tamPag;

                    var reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        ret.Add(new UnidadeMedidaModel
                        {
                            Id = (int)reader["id"],
                            Sigla=(string)reader["sigla"],
                            Nome = (string)reader["nome"],
                            Ativo = (bool)reader["ativo"]
                        });
                    }
                }
            }
            return ret;
        }
        public static UnidadeMedidaModel RecuperarPeloId(int id)
        {
            UnidadeMedidaModel ret = null;

            //criar objeto conexão
            using (var conexao = new SqlConnection())
            {
                //dados da conexão
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;

                //abrir conexão
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    //conectar comando com a conexao no banco
                    comando.Connection = conexao;

                    //comando
                    comando.CommandText = "select * from unidade_medida where id=@id;";

                    comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    var reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        ret = new UnidadeMedidaModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            Sigla =(string)reader["sigla"],
                            Ativo = (bool)reader["ativo"]
                        };
                    }
                }
            }
            return ret;
        }

        public static bool ExcluirPeloId(int id)
        {
            bool ret = false;

            if (RecuperarPeloId(id) != null)
            {

                //criar objeto conexão
                using (var conexao = new SqlConnection())
                {
                    //dados da conexão
                    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;

                    //abrir conexão
                    conexao.Open();

                    using (var comando = new SqlCommand())
                    {
                        //conectar comando com a conexao no banco
                        comando.Connection = conexao;

                        //comando
                        comando.CommandText = "delete from unidade_medida where id=@id;";

                        comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        ret = (comando.ExecuteNonQuery() > 0);

                    }
                }
            }
            return ret;
        }

        public int Salvar()
        {
            var ret = 0;
            var model = RecuperarPeloId(this.Id);

            //criar objeto conexão
            using (var conexao = new SqlConnection())
            {
                //dados da conexão
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;

                //abrir conexão
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    //conectar comando com a conexao no banco
                    comando.Connection = conexao;
                    if (model == null)
                    {

                        //comando
                        comando.CommandText = "Insert into unidade_medida (nome, sigla, ativo) values (@nome, @sigla, @ativo); select convert(int, scope_identity());";

                        //parametros para impedir sql injection
                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = this.Nome;
                        comando.Parameters.Add("@sigla", SqlDbType.VarChar).Value = this.Sigla;
                        comando.Parameters.Add("@ativo", SqlDbType.Bit).Value = (this.Ativo ? 1 : 0);

                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.CommandText = "update unidade_medida set nome =@nome, sigla =@sigla, ativo=@ativo where id=@id;";

                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = this.Nome;
                        comando.Parameters.Add("@sigla", SqlDbType.VarChar).Value = this.Sigla;
                        comando.Parameters.Add("@ativo", SqlDbType.Bit).Value = (this.Ativo ? 1 : 0);
                        comando.Parameters.Add("@id", SqlDbType.Int).Value = this.Id;

                        if (comando.ExecuteNonQuery() > 0)
                        {
                            ret = this.Id;
                        }
                    }

                }
            }
            return ret;
        }
    }
}