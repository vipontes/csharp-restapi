﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Aplicacao;
using RestfulAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestfulAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApiContext _contexto;

        public UsuarioController(ApiContext contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            return Ok("Index API");
        }

        [HttpPost]
        [Route("InsertUser")]
        public IActionResult InsertUser([FromBody] Usuario usuarioEnviado)
        {
            try
            {
                if (!ModelState.IsValid || usuarioEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new UsuarioAplicacao(_contexto).InsertUser(usuarioEnviado);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpPut]
        [Route("UpdateUser")]
        public IActionResult UpdateUser([FromBody] Usuario usuarioEnviado)
        {
            try
            {
                if (!ModelState.IsValid || usuarioEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new UsuarioAplicacao(_contexto).UpdateUser(usuarioEnviado);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }


        [HttpPost]
        [Route("GetUserByEmail")]
        public IActionResult GetClienteByEmail([FromBody] string email)
        {
            try
            {
                if (email == string.Empty)
                {
                    return BadRequest("Email inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new UsuarioAplicacao(_contexto).GetUserByEmail(email);

                    if (resposta != null)
                    {
                        var usuarioResposta = JsonConvert.SerializeObject(resposta);
                        return Ok(usuarioResposta);
                    }
                    else
                    {
                        return BadRequest("Usuário não cadastrado!");
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllClientes()
        {
            try
            {
                var listaDeUsuarios = new UsuarioAplicacao(_contexto).GetAllUsers();

                if (listaDeUsuarios != null)
                {
                    var resposta = JsonConvert.SerializeObject(listaDeUsuarios);
                    return Ok(resposta);
                }
                else
                {
                    return BadRequest("Nenhum usuário cadastrado!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpDelete]
        [Route("DeleteUserByEmail")]
        public IActionResult DeleteUserByEmail([FromBody] string email)
        {
            try
            {
                if (email == string.Empty)
                {
                    return BadRequest("Email inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new UsuarioAplicacao(_contexto).DeleteUserByEmail(email);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

    }
}
