Shader "Portal/PortalWindow"
{
    SubShader
    {
        Zwrite off
        Colormask 0
        cull off

        Stencil 
        {
            Ref 1
            Pass replace
        }

        Pass
        {

        }
    }
}
