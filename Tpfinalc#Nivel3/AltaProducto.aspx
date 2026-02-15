<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaProducto.aspx.cs" Inherits="Tpfinalc_Nivel3.AltaProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Alta de Producto</h2>

    <asp:Label ID="lblMsg" runat="server" CssClass="text-danger"></asp:Label>

    <div class="row mt-3">
        <div class="col-md-6 mb-3">
            <label>Código</label>
            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" />
        </div>

        <div class="col-md-6 mb-3">
            <label>Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
        </div>

        <div class="col-md-12 mb-3">
            <label>Descripción</label>
            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3"
                CssClass="form-control" />
        </div>

        <div class="col-md-6 mb-3">
            <label>Marca</label>
            <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select" />
        </div>

        <div class="col-md-6 mb-3">
            <label>Categoría</label>
            <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" />
        </div>

        <div class="col-md-8 mb-3">
            <label>Imagen URL</label>
            <asp:TextBox ID="txtImagenUrl" runat="server" CssClass="form-control"
                AutoPostBack="true" OnTextChanged="txtImagenUrl_TextChanged" />
        </div>

        <div class="col-md-4 mb-3">
            <label>Precio</label>
            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
        </div>

        <div class="col-md-12 mb-3">
            <img id="imgPreview" runat="server" class="img-fluid rounded"
                 style="max-height:250px;" onerror="this.src='../img/no-image.png'" />
        </div>

        <div class="col-md-12 d-flex gap-2">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar"
                CssClass="btn btn-success" OnClick="btnGuardar_Click" />
           <asp:Button ID="btnVolver" runat="server"
                Text="Volver"
                CssClass="btn btn-secondary"
                OnClick="btnVolver_Click" />
        </div>
    </div>

</asp:Content>