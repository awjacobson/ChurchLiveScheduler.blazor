## Auth0

- [Auth0 Dashboard](https://manage.auth0.com/dashboard/)
- [Quickstart](https://auth0.com/docs/quickstart/webapp/aspnet-core-blazor-server)

- __Step 1: Get the SVG Code__

  Find the icon you want (e.g., on Bootstrap Icons) and copy the SVG HTML.

  example [Google Fonts](https://fonts.google.com/icons?selected=Material+Symbols+Outlined)

- __Step 2: Convert the SVG to CSS__

  You must URL-encode the SVG code to use it inside a `url()` propery. You can use an online SVG to CSS converter to make this encoding easier for example [Convert SVG to CSS](https://yoksel.github.io/url-encoder/)

- __Step 3: Add CSS to `NavMenu.razor.css`__

  Open `Layout/NavMenu.razor.css` and create a new class.

  ```
  .bi-my-custom-icon-nav-menu {
    /* Replace the path data with your own SVG path */
    background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='white' class='bi bi-person' viewBox='0 0 16 16'%3E%3Cpath d='M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm2-3a2 2 0 1 1-4 0 2 2 0 0 1 4 0zm4 8c0 1-1 1-1 1H3s-1 0-1-1 1-4 6-4 6 3 6 4zm-1-.004c-.001-.246-.154-.986-.832-1.664C11.516 10.68 10.289 10 8 10c-2.29 0-3.516.68-4.168 1.332-.678.678-.83 1.418-.832 1.664h10z'/%3E%3C/svg%3E");
  }
  ```

- __Step 4: Update `NavMenu.razor`__

  Apply your new class to a `NavLink` inside the `NavMenu.razor` file:

  ```
  <div class="nav-item px-3">
    <NavLink class="nav-link" href="custom-page">
        <span class="bi bi-my-custom-icon-nav-menu" aria-hidden="true"></span> Custom Page
    </NavLink>
  </div>
  ```

- full instructions: [Add icons to Blazor’s NavMenu in .NET 8](https://mattfrear.com/2024/02/27/customize-blazors-navmenu/#:~:text=February%2027%2C%202024%20May%2021,which%20should%20give%20you)