# FontAwesome

[![GitHub License](https://img.shields.io/github/license/panoukos41/FontAwesome)](LICENSE)
[![Publish](https://github.com/panoukos41/FontAwesome/actions/workflows/publish.yaml/badge.svg)](https://github.com/panoukos41/FontAwesome/actions/workflows/publish.yaml)
[![NuGet](https://buildstats.info/nuget/P41.FontAwesome)](https://www.nuget.org/packages/P41.FontAwesome)

Razor components for the free [FontAwesome](https://fontawesome.com/) icons that incorporate the svgs as is.

Svgs are downlaoded from the [latest release on GitHub](https://github.com/FortAwesome/Font-Awesome/releases/latest).

# Icon Naming

All generated are based on the fontawesome naming. The they start with `Fa` followed by their group `Regular`, `Brands` etc and they end with their name `Star`, `ArrowUp` etc. All icon names are converted to Pascal Case.

## Examples

> Optional import the namespace `P41.FontAwesome` in your blazor `_imports.razor` file.

Using the icon [`github`](https://fontawesome.com/icons/github?f=brands&s=solid) in the `brands` category: 
```html
<FaBrandsGithub />
```

Using the icon [`circle-user`](https://fontawesome.com/icons/circle-user?f=classic&s=regular) in the `regular` category:
```html
<FaRegularCircleUser />
```

Using the icon [`cart-shopping`](https://fontawesome.com/icons/cart-shopping?f=classic&s=solid) in the `solid` category:
```html
<FaSolidCartShopping />
```

# Versioning

Versions follow the FontAwesome [releases](https://github.com/FortAwesome/Font-Awesome/releases) versions with an optional `revision` appended to it for cases fixes are done to the generation code.
