<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                exclude-result-prefixes="msxsl ext"
                 xmlns:ext="http://XsltSampleSite.XsltFunctions/1.0"
  >

  
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="/">
    <ul class="nav">
      <xsl:for-each select="UserProfile/Module">
        <li class="has-sub">
          <a href="{ext:GetUrl(Action,Controller, ../Area)}">
            <i class="{CssClass}"></i>
            <b class="caret pull-right"></b>
            <span>
              <xsl:value-of select="Name"/>
            </span>
            <xsl:if test="node()">
              <!--<b class="caret pull-right"></b>-->
            </xsl:if>
            <!--<span class="@Model.ModuleSelected"></span>-->
          </a>
          <xsl:if test="node()">
            <ul class="sub-menu">
              <xsl:for-each select="ModuleObject">
                <li class ="active">
                  <a href="{ext:GetUrl(Action,Controller, ../Area)}">
                    
                      <xsl:value-of select="Name"/>
                  
                    <!--<span class="@Model.ModuleSelected"></span>-->
                  </a>
                 </li>
              </xsl:for-each>
            </ul>
          </xsl:if>
        </li>
      </xsl:for-each>
    </ul>
  </xsl:template>
</xsl:stylesheet>
