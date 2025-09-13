document.addEventListener("DOMContentLoaded", function () {
  const themeToggle = document.getElementById("themeToggle");

  // Sayfa yüklenince localStorage kontrolü
  if (localStorage.getItem("theme") === "dark") {
    document.body.classList.add("dark-mode");
    if (themeToggle) {
      const icon = themeToggle.querySelector("i");
      if (icon) icon.classList.replace("fa-moon", "fa-sun");
    }
  }

  // Tema değiştirici buton
  if (themeToggle) {
    themeToggle.addEventListener("click", () => {
      document.body.classList.toggle("dark-mode");
      const icon = themeToggle.querySelector("i");
      if (icon) {
        icon.classList.toggle("fa-moon");
        icon.classList.toggle("fa-sun");
      }
      localStorage.setItem(
        "theme",
        document.body.classList.contains("dark-mode") ? "dark" : "light"
      );
    });
  }

  // Navbar collapse otomatik kapanması
  const navbarCollapse = document.querySelector(".navbar-collapse");
    document
    .querySelectorAll(".navbar-nav .nav-link:not(.dropdown-toggle)")
    .forEach((link) => {
        link.addEventListener("click", () => {
        if (navbarCollapse && navbarCollapse.classList.contains("show")) {
            new bootstrap.Collapse(navbarCollapse).hide();
        }
        });
    });

  // Search input Enter ile submit
  document.querySelectorAll(".search-form").forEach((form) => {
    const input = form.querySelector(
      'input[name="name"], input[name="search"]'
    );
    if (!input) return;
    input.addEventListener("keydown", function (e) {
      if (e.key === "Enter") {
        e.preventDefault();
        if (input.value.trim() !== "") form.submit();
      }
    });
  });

  // Sayfa geri gelince search inputları temizle
  window.addEventListener("pageshow", function () {
    document
      .querySelectorAll(".search-form input")
      .forEach((i) => (i.value = ""));
  });
});
