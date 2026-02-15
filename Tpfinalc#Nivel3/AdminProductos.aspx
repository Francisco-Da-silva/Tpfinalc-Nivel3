<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminProductos.aspx.cs" Inherits="Tpfinalc_Nivel3.AdminProductos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Administración de Productos</h2>

    <div class="row mb-3">
        <div class="col-md-4">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" Placeholder="Buscar..." />
        </div>
        <div class="col-md-3">
            <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select" />
        </div>
        <div class="col-md-3">
            <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" />
        </div>
        <div class="col-md-2 d-flex gap-2">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary w-100" OnClick="btnBuscar_Click"/>
        </div>
    </div>

    <div class="mb-3">
        <a class="btn btn-success" href="AltaProducto.aspx">+ Nuevo producto</a>


    </div>

    <asp:GridView ID="gvArticulos" runat="server" CssClass="table table-striped"
        AutoGenerateColumns="false" DataKeyNames="Id" OnRowCommand="gvArticulos_RowCommand">
        <Columns>
            <asp:BoundField DataField="Codigo" HeaderText="Código" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="MarcaDescripcion" HeaderText="Marca" />
            <asp:BoundField DataField="CategoriaDescripcion" HeaderText="Categoría" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:N2}" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <a class="btn btn-sm btn-warning" href='ProductoForm.aspx?id=<%# Eval("Id") %>'>Editar</a>
                    <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-danger"
                        CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'
                        OnClientClick="return confirm('¿Seguro que querés eliminar este producto?');">
                        Eliminar
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>