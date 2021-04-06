using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Controle_de_estoque.Models
{
    public class GrupoProdutoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Preencha o campo nome.")]
        public string Nome { get; set; }
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
                    comando.CommandText = "select count(*) from grupo_produto";


                    ret = (int)comando.ExecuteScalar();
                }
            }
            return ret;
        }

        public static List<GrupoProdutoModel> RecuperarLista(int pag, int tamPag)
        {
            var ret = new List<GrupoProdutoModel>();

            //criar objeto conexão
            using (var conexao = new SqlConnection())
            {
                //dados da conexão
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;

                //abrir conexão
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    var pos = (pag-1) * tamPag;

                    //conectar comando com a conexao no banco
                    comando.Connection = conexao;

                    //comando]
                    //offset = apartir de qual registro devo retornar
                    //rows fetch next  = quantos registros devo retornar
                    comando.CommandText = "select * from grupo_produto order by Nome " +
                        "offset @pos rows fetch next @pag rows only;";

                    comando.Parameters.Add("@pos", SqlDbType.Int).Value = pos>0?pos-1:0;
                    comando.Parameters.Add("@pag", SqlDbType.Int).Value = tamPag;

                    var reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        ret.Add(new GrupoProdutoModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            Ativo = (bool)reader["ativo"]
                        });
                    }
                }
            }
            return ret;
        }
        public static GrupoProdutoModel RecuperarPeloId(int id)
        {
            GrupoProdutoModel ret = null;

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
                    comando.CommandText = "select * from grupo_produto where id=@id;";

                    comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    var reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        ret = new GrupoProdutoModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
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
                        comando.CommandText = "delete from grupo_produto where id=@id;";

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
                        comando.CommandText = "Insert into grupo_produto (nome,ativo) values (@nome, @ativo); select convert(int, scope_identity());";
                        
                        //parametros para impedir sql injection
                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = this.Nome;
                        comando.Parameters.Add("@ativo", SqlDbType.Bit).Value = (this.Ativo ? 1 : 0);

                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.CommandText = "update grupo_produto set nome =@nome, ativo=@ativo where id=@id;";
                        
                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = this.Nome;
                        comando.Parameters.Add("@ativo", SqlDbType.Bit).Value = (this.Ativo ? 1 : 0);
                        comando.Parameters.Add("@id", SqlDbType.Int).Value = this.Id;

                        if (comando.ExecuteNonQuery()>0)
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