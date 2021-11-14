using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using wsteste.Models;
using wsteste.Dao;

namespace wsteste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private PessoaDao pessoaDao;
        
        public PessoaController()
        {
            if( pessoaDao == null )
            {
                pessoaDao = new PessoaDao();
            }
        }

        [HttpGet]
        public IActionResult getTodasPessoas()
        {
            if (!pessoaDao.GetListaDePessoas(out List<Pessoa> lista))
            {
                return NotFound("Não há pessoas cadastradas");
            }
            return Ok(lista);
        }
        
        [HttpGet("byCPF/{cpf}")]
        public IActionResult getPessoaByCPF(string cpf)
        {
            if( cpf == null || cpf == "" )
            {
                return BadRequest();
            }

            Pessoa pessoa = pessoaDao.GetPessoaPorCpf(cpf);

            if(pessoa != null)
            {
                return Ok(pessoa);
            }
            return NotFound("Pessoa não encontrada para o CPF informado!");
        }

        [HttpPost]
        public IActionResult inserirPessoaCompleta(Pessoa novaPessoa)
        {
            if(!pessoaDao.CadastrarNovaPessoa(novaPessoa, out string message))
            {
                return BadRequest(message);
            }
            return Ok(message);
        }

        [HttpDelete]
        public IActionResult ExcluiPessoa(string cpf) 
        {
            if(!pessoaDao.ExcluirPessoaPorCpf(cpf, out string message))
            {
                return BadRequest(message);
            }
            return Ok(message);
        }

        [HttpPut]
        public IActionResult AlterarDadosPessoa(Pessoa pessoa)
        {
            if(!pessoaDao.AtualizaPessoa(pessoa, out string message))
            {
                return BadRequest(message);
            }
            return Ok(message);
        }
    }
}
