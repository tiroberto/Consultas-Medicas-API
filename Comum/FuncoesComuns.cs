using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Comum
{
    public class FuncoesComuns
    {
        public static bool ValidarCPF(string _valor)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            _valor = _valor.Trim();
            _valor = _valor.Replace(".", "").Replace("-", "");
            if (_valor.Length != 11)
                return false;
            tempCpf = _valor.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return _valor.EndsWith(digito);
        }

        public static bool ValidarEmail(string email)
        {
            var pattern = @"^(?!.*\.\.)(?!.*\.$)[a-zA-Z0-9](\.?[a-zA-Z0-9_\-+])*@[a-zA-Z0-9\-]+(\.[a-zA-Z]{2,})+$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool ValidarTelefone(string telefone)
        {
            var pattern = @"^\(?[1-9]{2}\)?\s?(9\d{4}|[2-5]\d{3})-?\d{4}$";
            return Regex.IsMatch(telefone, pattern);
        }       

    }
}
