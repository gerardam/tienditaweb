using System;
using System.Data;
using VentasCal.Datos.Recursos;

//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace VentasCal.Datos
{
    internal class PersonalFactoria : IDomainObjectFactory<Usuario>
    {
        /// <summary>
        /// Metodo que construye un objeto Usuario de un datareader.
        /// </summary>
        /// <param name="reader">Objeto tipo IDataReader con el que se contruira el objeto personal.</param>
        /// <returns>Regresa un Objeto personal.</returns>
        public Usuario Construct(IDataReader reader)
        {
            if (reader == null) throw new Exception(CamposConstantes.CC_ObjetoLectura);
            Usuario personal = new Usuario();

            int claveEntidadIndex = reader.GetOrdinal(CamposConstantes.CC_ClaveEntidad);
            if (!reader.IsDBNull(claveEntidadIndex))
                personal.ClaveEntidad = reader.GetInt32(claveEntidadIndex);

            int idUsuarioIndex = reader.GetOrdinal(CamposConstantes.CC_CorreoElectronico);
            if (!reader.IsDBNull(idUsuarioIndex))
                personal.IdUsuario = reader.GetString(idUsuarioIndex);

            int nombreIndex = reader.GetOrdinal(CamposConstantes.CC_Nombre);
            if (!reader.IsDBNull(nombreIndex))
                personal.Nombre = reader.GetString(nombreIndex);

            int apellidoPIndex = reader.GetOrdinal(CamposConstantes.CC_ApellidoPaterno);
            if (!reader.IsDBNull(apellidoPIndex))
                personal.ApellidoPaterno = reader.GetString(apellidoPIndex);

            int apellidoMIndex = reader.GetOrdinal(CamposConstantes.CC_ApellidoMaterno);
            if (!reader.IsDBNull(apellidoMIndex))
                personal.ApellidoMaterno = reader.GetString(apellidoMIndex);

            int telefonoIndex = reader.GetOrdinal(CamposConstantes.CC_Telefono);
            if (!reader.IsDBNull(telefonoIndex))
                personal.Telefono = reader.GetString(telefonoIndex);

            int tipoUsuarioIndex = reader.GetOrdinal(CamposConstantes.CC_TipoUsuario);
            if (!reader.IsDBNull(tipoUsuarioIndex))
                personal.TipoUsuario = reader.GetByte(tipoUsuarioIndex);

            int claveEstadoIndex = reader.GetOrdinal(CamposConstantes.CC_ClaveEstado);
            if (!reader.IsDBNull(claveEstadoIndex))
                personal.ClaveEstado = reader.GetByte(claveEstadoIndex);

            int fechaCambioIndex = reader.GetOrdinal(CamposConstantes.CC_FechaCambio);
            if (!reader.IsDBNull(fechaCambioIndex))
                personal.FechaCambio = reader.GetDateTime(fechaCambioIndex);

            return personal;
        }
    }
}