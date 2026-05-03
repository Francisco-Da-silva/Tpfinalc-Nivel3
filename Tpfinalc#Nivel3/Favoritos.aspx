<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="Tpfinalc_Nivel3.Favoritos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="page-title">
    <h1>Mis favoritos</h1>
    <p>Productos guardados para volver a verlos rapido.</p>
</div>

<div class="row product-grid">

<asp:Repeater ID="repFavoritos" runat="server">
<ItemTemplate>

    <div class="col-sm-6 col-lg-3">
        <div class="card product-card h-100">

            <img src='<%# GetImagenProducto(Eval("ImagenUrl")) %>'
                 class="product-image"
                 onerror="this.onerror=null;this.src='img/no-image.png';" />

            <div class="card-body text-center">
                <h6 class="product-name"><%# Eval("Nombre") %></h6>
                <p class="product-price">$ <%# Eval("Precio") %></p>

                <asp:Button ID="btnQuitar" runat="server"
                    Text="Quitar"
                    CommandArgument='<%# Eval("Id") %>'
                    OnCommand="btnQuitar_Command"
                    CssClass="btn btn-danger btn-sm w-100"/>
            </div>

        </div>
    </div>

</ItemTemplate>
</asp:Repeater>

</div>

</asp:Content>
