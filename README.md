# TP Final - Catálogo Web (ASP.NET WebForms + SQL Server)

Aplicación web para un comercio/emprendimiento que permite mostrar un catálogo de productos a potenciales clientes, con navegación amigable, filtros de búsqueda, pantalla de detalle, login y área de administración para gestión de productos (solo admins).

## Funcionalidades requeridas (según consigna)

### Portal público
- **Home** con catálogo de productos (cards) y filtros:
  - Búsqueda por texto (nombre / descripción según implementación)
  - Filtro por **Marca**
  - Filtro por **Categoría**
- **Detalle de producto** accesible desde el catálogo

### Seguridad y administración
- **Login** (usuarios almacenados en tabla `USERS`)
- **Área de administración** (solo usuarios con `admin = 1`):
  - Listado de artículos (formato grilla)
  - Búsqueda por distintos criterios
  - **Agregar** artículo
  - **Modificar** artículo
  - **Eliminar** artículo

### Opcional (no obligatorio)
- Registro de cliente
- Mi Perfil
- Mis Favoritos (tabla `FAVORITOS`)

---

## Tecnologías
- ASP.NET WebForms
- SQL Server (SSMS 19)
- ADO.NET (SqlConnection / SqlCommand / SqlDataReader)
- Stored Procedures
- Bootstrap (UI)

---

## Base de datos

### Nombre de la base
`CATALOGO_WEB_DB`

### Tablas
- `ARTICULOS`
- `MARCAS`
- `CATEGORIAS`
- `USERS`
- `FAVORITOS`

### Script de creación
Ejecutar en SSMS el script provisto por la cátedra/consigna (crea DB, tablas e inserts de prueba).

---

## Stored Procedures (en uso / sugeridos)

### Catálogo (público)
- `SP_ListarArticulos`  
  Lista artículos con filtros por texto, marca y categoría.
- `SP_ListarMarcas`  
  Lista marcas para DropDownList.
- `SP_ListarCategorias`  
  Lista categorías para DropDownList.
- `SP_ArticuloPorId`  
  Trae el detalle de un artículo (incluye Marca/Categoría por JOIN).

### Admin (ABM)
- `SP_AgregarArticulo`
- `SP_ModificarArticulo`
- `SP_EliminarArticulo`

*(Estos se implementan para cumplir alta/modificación/baja desde el área admin.)*

---

## Arquitectura en capas

Estructura sugerida:
- **Dominio**: clases del modelo (Articulo, Marca, Categoria, User)
- **DAL / Conexion**: acceso a datos (ArticuloDAL, MarcaDAL, CategoriaDAL, UserDAL)
- **Web**: páginas (Home, Detalle, Login, Admin)

Criterios aplicados:
- El **DAL** contiene `try/catch` y relanza excepciones con contexto (`throw new Exception(..., ex)`).
- Las **páginas** capturan errores y redirigen a `Error.aspx` guardando el mensaje en `Session["Error"]`.
- Validaciones mínimas en UI (campos obligatorios, tipos numéricos, etc.).

---

## Manejo de errores
- Uso sistemático de `try/catch`.
- Redirección a `Error.aspx` cuando ocurre un error.
- `Error.aspx` muestra mensaje almacenado en `Session["Error"]`.

---

## Pantallas

### Home.aspx (público)
- Catálogo en formato cards (imagen + nombre + precio)
- Filtros por:
  - Texto
  - Marca
  - Categoría
- Mensaje cuando no hay resultados para el filtro seleccionado
- Link a detalle: `Detalle.aspx?id={IdArticulo}`

### Detalle.aspx (público)
- Imagen grande (con `onerror` para imagen por defecto)
- Nombre, precio, código
- Marca y categoría (obtenidas por SP con JOIN)
- Descripción
- Botón volver al Home

### Login.aspx
- Inicio de sesión contra tabla `USERS`
- Guarda usuario en sesión
- Redirige según rol:
  - admin => área admin
  - no admin => portal público

### Admin (solo admin)
- Listado grilla de artículos
- Búsqueda
- Alta / Modificación / Baja

---

## Usuarios de prueba
Insertados por script:
- Admin: `admin@admin.com` / `admin` (admin = 1)
- User: `user@user.com` / `user` (admin = 0)

---

## Configuración

### Connection String
Actualizar en las clases DAL según instancia local:

Ejemplo:
```txt
Server=.\SQLEXPRESS;Database=CATALOGO_WEB_DB;Trusted_Connection=True;TrustServerCertificate=True
