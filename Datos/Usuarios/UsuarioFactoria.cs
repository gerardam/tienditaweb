using System;
using System.Data;
using VentasCal.Datos.Recursos;

//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace VentasCal.Datos
{
    internal class UsuarioFactoria : IDomainObjectFactory<Usuario>
    {
        /// <summary>
        /// Metodo que construye un objeto Usuario de un datareader.
        /// </summary>
        /// <param name="reader">Objeto tipo IDataReader con el que se contruira el objeto usuario.</param>
        /// <returns>Regresa un Objeto Usuario.</returns>
        public Usuario Construct(IDataReader reader)
        {
            if (reader == null) throw new Exception(CamposConstantes.CC_ObjetoLectura);
            Usuario usuario = new Usuario();

            int claveEntidadIndex = reader.GetOrdinal(CamposConstantes.CC_ClaveEntidad);
            if (!reader.IsDBNull(claveEntidadIndex))
                usuario.ClaveEntidad = reader.GetInt32(claveEntidadIndex);

            int idUsuarioIndex = reader.GetOrdinal(CamposConstantes.CC_IdUsuario);
            if (!reader.IsDBNull(idUsuarioIndex))
                usuario.IdUsuario = reader.GetString(idUsuarioIndex);

            int tipoUsuarioIndex = reader.GetOrdinal(CamposConstantes.CC_TipoUsuario);
            if (!reader.IsDBNull(tipoUsuarioIndex))
                usuario.TipoUsuario = reader.GetByte(tipoUsuarioIndex);

            int claveEstadoIndex = reader.GetOrdinal(CamposConstantes.CC_ClaveEstado);
            if (!reader.IsDBNull(claveEstadoIndex))
                usuario.ClaveEstado = reader.GetByte(claveEstadoIndex);

            int fechaCambioIndex = reader.GetOrdinal(CamposConstantes.CC_FechaCambio);
            if (!reader.IsDBNull(fechaCambioIndex))
                usuario.FechaCambio = reader.GetDateTime(fechaCambioIndex);
            
            return usuario;
        }
    }
}