<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductoForm.aspx.cs" Inherits="Tpfinalc_Nivel3.ProductoForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-title">
        <h1><asp:Label ID="lblTitulo" runat="server" Text="Nuevo producto"></asp:Label></h1>
        <p>Actualiza la informacion visible en el catalogo.</p>
    </div>

    <div class="panel-card filter-panel">
        <div class="row g-4">
            <div class="col-md-6">
                <div class="mb-3">
                    <label class="form-label">Codigo</label>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Descripcion</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Precio</label>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
                </div>

                <div class="row g-3">
                    <div class="col-md-6">
                        <label class="form-label">Marca</label>
                        <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select" />
                    </div>

                    <div class="col-md-6">
                        <label class="form-label">Categoria</label>
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" />
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <div class="mb-3">
                    <label class="form-label">URL Imagen</label>
                    <asp:TextBox ID="txtImagenUrl" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtImagenUrl_TextChanged" />
                </div>

                <asp:Image ID="imgProducto" runat="server" CssClass="detail-image" />
            </div>
        </div>

        <div class="mt-4 d-flex gap-2">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
            <a href="AdminProductos.aspx" class="btn btn-secondary">Cancelar</a>
        </div>
    </div>

</asp:Content>
