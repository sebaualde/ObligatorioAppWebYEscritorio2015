<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:template match="/">

    <table align="Center">
      <tr style ="background-color: #FF0000">
        <td style ="color: #FFFFFF" align="Center"> Empresa </td>
        <td style ="color: #FFFFFF" align="Center"> Fecha de Vista </td>
      </tr>

      <xsl:for-each select="Raiz/Visita">
        <tr style ="background-color: #FFE6DF">
          <td>
            <xsl:value-of select="NomEmpresa" />
          </td>
          <td>
            <xsl:value-of select="Fecha" />
          </td>
        </tr>
      </xsl:for-each>

    </table>
  </xsl:template>
</xsl:stylesheet>
