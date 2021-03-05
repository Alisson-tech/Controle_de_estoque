﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Controle_de_estoque.Models
{
    public class UsuarioModel
    {
        //Verificar se o Usuário existe no banco
        public static bool ValidarUsuario(string login, string senha)
        {
            bool validar = false;

            //criar objeto conexão
            using (var conexao = new SqlConnection())
            {
                //dados da conexão (arquivo webconfig)
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;

                //abrir conexão
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    //conectar comando com a conexao no banco
                    comando.Connection = conexao;

                    //comando
                    comando.CommandText = string.Format("select count(*) from usuario where login='{0}'" +
                        " and senha='{1}'", login, CriptoHelper.HashMD5(senha)) ;
                    
                    //ExecuteScalar -> retorna a primeira linha do banco
                    //se a resposta for menor que 0 login não autorizado, caso ao contrario login autorizado
                    validar = ((int)comando.ExecuteScalar() > 0);
                }
            }
            return validar;
        }
    }
}