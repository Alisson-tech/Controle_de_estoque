using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Controle_de_estoque
{
    public static class CriptoHelper
    {
        public static string HashMD5(string val)
        {
            //converter string para bytes
            var bytes = Encoding.ASCII.GetBytes(val);

            //cria objeto de criptografia
            var md5 = MD5.Create();

            //cria o hash em bytes
            var hash = md5.ComputeHash(bytes);

            var ret = string.Empty;
            for (int i = 0; i < hash.Length; i++)
            {
                //pega cada hash em byte e transforma em uma string hexadecimal
                ret += hash[i].ToString("x2");
            }
            return ret;
        }   
    }
}