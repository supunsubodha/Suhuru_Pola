let currentSlideIndex = 0;
let slideTimer;

function showSlides(){
    let slides = document.getElementsByClassName("slide");
    let dots = document.getElementsByClassName('dot');

    if(slides.length === 0 ) return;

    // Hide all slides
    for(let i = 0; i < slides.length; i++){
        slides[i].style.display = "none";
    }

    // Reset dot safly
    for(let i = 0; i < dots.length; i++){
        dots[i].classList.remove("active");
    }

    // Increment slide index
    currentSlideIndex++;
    if(currentSlideIndex > slides.length){
        currentSlideIndex = 1;
    }

    // Show current slide and active dot
    slides[currentSlideIndex - 1].style.display = "block";

    //Highlight active dot safly
    if(dots.length >= currentSlideIndex){
        dots[currentSlideIndex - 1].classList.add("active");
    }

    // Change slide every 3 seconds
    slideTimer = setTimeout(showSlides, 3000);
}

// Manual slide selection via dots
function setSlide(index){
    clearTimeout(slideTimer);

    // Align 0-based index with show slide -1 based tracking
   // currentSlideIndex = index; //increment correctly
    let slides = document.getElementsByClassName("slide");
    let dots = document.getElementsByClassName("dot");

    if(slides.length === 0) return;

    for( let i = 0; i < slides.length; i++){
        slides[i].style.display = "none";
    }

    for( let i = 0; i < dots.length; i++){
        dots[i].classList.remove("active");
    }   

    slides[index].style.display = "block";
    if(dots[index]){
        dots[index].classList.add("active");
    }
    // Set currentSlidIndex to match 1-based tracking for the iteration
    currentSlideIndex = index + 1;
    
    //Restart auto play timer
    slideTimer = setTimeout(showSlides, 3000);
}

// Start slideshow when page loades
document.addEventListener("DOMContentLoaded", showSlides);

// ==========================================
// 1. DYNAMIC NEWS DATA
// ==========================================
const newsArticles = [
  {
    id: 1,
    title: "Organic Strawberries Arrive Early This Season",
    category: "harvest",
    categoryLabel: "Harvest Updates",
    date: "July 24, 2026",
    icon: "🍓",
    excerpt: "Thanks to warmer spring temperatures, our berry farmers in the southern valley have begun harvesting sweet organic strawberries two weeks ahead of schedule."
  },
  {
    id: 2,
    title: "Saturday Farmers Market: Live Music & Free Samples",
    category: "event",
    categoryLabel: "Market Events",
    date: "July 22, 2026",
    icon: "🎪",
    excerpt: "Join us this weekend at the Green Valley Hub! We will feature over 30 local vendor stalls, live acoustic music, and free cheese tasting from Highland Pastures."
  },
  {
    id: 3,
    title: "Meet the Farmer: How Sunil Built a Zero-Waste Orchard",
    category: "story",
    categoryLabel: "Farmer Stories",
    date: "July 18, 2026",
    icon: "👨‍🌾",
    excerpt: "Sunil Silva started with just three mango trees in 2018. Today, his family runs a 10-acre organic orchard using solar irrigation and natural composting."
  },
  {
    id: 4,
    title: "New Harvest: Heirloom Carrots & Root Vegetables",
    category: "harvest",
    categoryLabel: "Harvest Updates",
    date: "July 15, 2026",
    icon: "🥕",
    excerpt: "Root & Soil Co-op has just delivered a fresh batch of purple, yellow, and orange heirloom carrots. Perfect for autumn roasting and healthy snacking!"
  },
  {
    id: 5,
    title: "Workshop: Learn Urban Beekeeping & Honey Harvesting",
    category: "event",
    categoryLabel: "Market Events",
    date: "July 10, 2026",
    icon: "🐝",
    excerpt: "Ever wanted to harvest your own raw honey? BeeHappy Apiaries is hosting a beginner-friendly 2-hour workshop next Sunday at the main greenhouse."
  },
  {
    id: 6,
    title: "Why Supporting Local Co-ops Reduces Carbon Footprints",
    category: "story",
    categoryLabel: "Farmer Stories",
    date: "July 05, 2026",
    icon: "🌍",
    excerpt: "When you buy directly from local farmers, your food travels an average of 30 miles instead of 1,500 miles. Read how our community is making a difference."
  }
];

// ==========================================
// 2. INITIALIZE ON PAGE LOAD
// ==========================================
document.addEventListener("DOMContentLoaded", () => {
  renderNews("all");
  setupNewsFilters();
  setupContactForm();
});

// ==========================================
// 3. NEWS SECTION LOGIC
// ==========================================
function renderNews(filterCategory) {
  const newsGrid = document.getElementById("news-grid-container");
  if (!newsGrid) return;
  
  newsGrid.innerHTML = ""; // Clear existing

  // Filter the array
  const filteredArticles = filterCategory === "all"
    ? newsArticles
    : newsArticles.filter(article => article.category === filterCategory);

  // Generate cards
  filteredArticles.forEach(article => {
    const card = document.createElement("div");
    card.className = "news-card";
    card.innerHTML = `
      <div class="news-banner">
        <span class="news-category-tag">${article.categoryLabel}</span>
        <span>${article.icon}</span>
      </div>
      <div class="news-content">
        <span class="news-date">🗓️ ${article.date}</span>
        <h3 class="news-title">${article.title}</h3>
        <p class="news-excerpt">${article.excerpt}</p>
        <button class="read-more-btn" onclick="readMoreAlert('${article.title}')">
          Read Full Story →
        </button>
      </div>
    `;
    newsGrid.appendChild(card);
  });
}

function setupNewsFilters() {
  const filterButtons = document.querySelectorAll("#news-section .filter-btn");
  
  filterButtons.forEach(btn => {
    btn.addEventListener("click", (e) => {
      // Remove active state from all buttons
      filterButtons.forEach(b => b.classList.remove("active"));
      // Add active state to clicked button
      e.target.classList.add("active");
      
      // Get filter value and re-render
      const filterValue = e.target.getAttribute("data-filter");
      renderNews(filterValue);
    });
  });
}

// Simple interactive modal/alert when clicking "Read More"
function readMoreAlert(title) {
  alert(`📖 Opening Article:\n"${title}"\n\n(This would direct the user to the full article page or open a pop-up modal in your complete project!)`);
}

// ==========================================
// 4. CONTACT FORM VALIDATION & LOGIC
// ==========================================
function setupContactForm() {
  const form = document.getElementById("farmer-contact-form");
  const statusMsg = document.getElementById("form-status");

  if (!form) return;

  form.addEventListener("submit", (e) => {
    e.preventDefault(); // Prevent page reload

    const name = document.getElementById("name").value.trim();
    const email = document.getElementById("email").value.trim();
    const role = document.getElementById("role").value;
    const message = document.getElementById("message").value.trim();

    // Check if fields are empty
    if (!name || !email || !role || !message) {
      showStatus(statusMsg, "Please fill out all required fields before sending.", "error");
      return;
    }

    // Basic email format check
    if (!email.includes("@") || !email.includes(".")) {
      showStatus(statusMsg, "Please enter a valid email address.", "error");
      return;
    }

    // Success state
    showStatus(
      statusMsg, 
      `🌱 Thank you, ${name}! Your message has been sent to our farm network. We will get back to you soon!`, 
      "success"
    );

    // Clear form inputs
    form.reset();

    // Hide message after 6 seconds
    setTimeout(() => {
      statusMsg.classList.add("hidden");
    }, 6000);
  });
}

function showStatus(element, text, type) {
  element.textContent = text;
  element.className = `status-msg ${type}`; // Removes 'hidden' and applies color class
}