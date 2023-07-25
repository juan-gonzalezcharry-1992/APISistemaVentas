using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepositorio;
        private readonly IMapper _mapper;

        public UsuarioService(IGenericRepository<Usuario> usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> Lista()
        {
            try {

                var queryUsuario = await _usuarioRepositorio.Consultar();
                var listaUsuarios = queryUsuario.Include(rol => rol.IdRolNavigation).ToList();

                return _mapper.Map<List<UsuarioDTO>>(listaUsuarios);

            }catch(Exception ex) {
                throw;
            }
        }

        public async Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {

            try {
                var queryUsuario = await _usuarioRepositorio.Consultar(u => 
                 u.Correo == correo &&
                 u.Clave == clave
                );

                if(queryUsuario.FirstOrDefault() == null)
                {
                    throw new TaskCanceledException("El usuario no existe");
                }

                Usuario devolverUsuario = queryUsuario.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<SesionDTO>(devolverUsuario);
            }
            catch(Exception ex) {
                throw new NotImplementedException(); 
            }
            
        }

        public async Task<UsuarioDTO> Crear(UsuarioDTO modelo)
        {
            try {
                var UsuarioCreado = await _usuarioRepositorio.Crear(_mapper.Map<Usuario>(modelo));

                if (UsuarioCreado.IdUsuario == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el usuario");
                }

                var query = await _usuarioRepositorio.Consultar(u => u.IdUsuario == UsuarioCreado.IdUsuario);

                UsuarioCreado = query.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<UsuarioDTO>(UsuarioCreado);

            } catch {
                throw;
            
            }
        }

        public async Task<bool> Editar(UsuarioDTO modelo)
        {

            try {
                var UsuarioModelo = _mapper.Map<Usuario>(modelo);
                var UsuarioEncontrado = await _usuarioRepositorio.Obtener(u => u.IdUsuario == UsuarioModelo.IdUsuario);

                if(UsuarioEncontrado == null)
                    throw new TaskCanceledException("El usuario no existe");


                UsuarioEncontrado.NombreCompleto = UsuarioModelo.NombreCompleto;
                UsuarioEncontrado.Correo = UsuarioModelo.Correo;
                UsuarioEncontrado.IdRol = UsuarioModelo.IdRol;
                UsuarioEncontrado.Clave =UsuarioModelo.Clave;
                UsuarioEncontrado.EsActivo = UsuarioModelo.EsActivo;
               
                bool  respuesta =  await _usuarioRepositorio.Editar(UsuarioEncontrado);

                if(!respuesta)
                    throw new TaskCanceledException("No se pudo editar el usuario");

                return respuesta;


            } catch { 
                    throw;
            }
            
        }

        public async Task<bool> Eliminar(int id)
        {
            try {

                var usuarioEncontrado = await _usuarioRepositorio.Obtener(u => u.IdUsuario == id);

                if(usuarioEncontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                bool respuesta = await _usuarioRepositorio.Eliminar(usuarioEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar el Eliminar");

                return respuesta;

            } catch { 
                
                throw new NotImplementedException();
            }
            
        }

        
    }
}
