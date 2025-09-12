document.addEventListener("DOMContentLoaded", function () {
    // ===== DARK MODE TOGGLE =====
    const themeToggle = document.getElementById("themeToggle");
    if (themeToggle) {
        themeToggle.addEventListener("click", () => {
            document.body.classList.toggle("dark-mode");
            const icon = themeToggle.querySelector("i");
            if (icon) {
                icon.classList.toggle("fa-moon");
                icon.classList.toggle("fa-sun");
            }
        });
    }

    // ===== NAVBAR COLLAPSE AUTO-CLOSE ON LINK CLICK (MOBILE) =====
    const navbarCollapse = document.querySelector('.navbar-collapse');
    document.querySelectorAll('.navbar-nav .nav-link').forEach(link => {
        link.addEventListener('click', () => {
            if (navbarCollapse && navbarCollapse.classList.contains('show')) {
                new bootstrap.Collapse(navbarCollapse).hide();
            }
        });
    });

    // ===== DROPDOWN MENUS =====
    document.querySelectorAll('.dropdown-toggle').forEach(toggle => {
        toggle.addEventListener('click', (e) => {
            e.preventDefault();
            const parentDropdown = toggle.closest('.dropdown');
            const menu = parentDropdown.querySelector('.dropdown-menu');
            if (!menu) return;

            // Close other open dropdowns
            document.querySelectorAll('.dropdown-menu.show').forEach(openMenu => {
                if (openMenu !== menu) openMenu.classList.remove('show');
            });

            menu.classList.toggle('show');
        });
    });

    // ===== CLOSE DROPDOWN IF CLICKED OUTSIDE =====
    document.addEventListener('click', (e) => {
        document.querySelectorAll('.dropdown-menu.show').forEach(menu => {
            if (!menu.closest('.dropdown').contains(e.target)) {
                menu.classList.remove('show');
            }
        });
    });
});
