<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductoForm.aspx.cs" Inherits="Tpfinalc_Nivel3.ProductoForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Producto</title>

    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>

    <form id="form1" runat="server">
        <div class="container mt-5">

            <h2 class="mb-4">
                <asp:Label ID="lblTitulo" runat="server" Text="Nuevo Producto"></asp:Label>
            </h2>

            <div class="row">

                <!-- IZQUIERDA -->
                <div class="col-md-6">

                    <div class="mb-3">
                        <label>Código</label>
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" />
                    </div>

                    <div class="mb-3">
                        <label>Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    </div>

                    <div class="mb-3">
                        <label>Descripción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" />
                    </div>

                    <div class="mb-3">
                        <label>Precio</label>
                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
                    </div>

                    <div class="mb-3">
                        <label>Marca</label>
                        <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control" />
                    </div>

                    <div class="mb-3">
                        <label>Categoría</label>
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control" />
                    </div>

                </div>

                <!-- DERECHA -->
                <div class="col-md-6">

                    <div class="mb-3">
                        <label>URL Imagen</label>
                        <asp:TextBox ID="txtImagenUrl" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtImagenUrl_TextChanged" />
                    </div>

                    <div class="text-center">
                        <asp:Image ID="imgProducto" runat="server" Width="300px" CssClass="img-thumbnail" />
                    </div>

                </div>

            </div>

            <div class="mt-4">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                <a href="AdminProductos.aspx" class="btn btn-secondary">Cancelar</a>
            </div>

        </div>
    </form>

</body>
</html>