# Shinobicalypse: Ninja Runner

Shinobicalypse: Ninja Runner is a fast-paced mobile endless runner developed in Unity and C#.

The project focuses on scalable gameplay systems, responsive game feel, and AI-driven mechanics designed for mobile performance.

---

[![ShinobiCalypse Gameplay](https://img.youtube.com/vi/U0l76m79inw/0.jpg)](https://www.youtube.com/shorts/U0l76m79inw)

## 🎮 Core Gameplay Systems

- Lane-based movement system optimized for mobile input responsiveness  
- Endless progression loop with dynamic difficulty scaling  
- Obstacle interaction and collision-based gameplay mechanics  
- Tight and responsive player control system  

---

## ⚙️ Gameplay Architecture

### 🧠 AI State Machine System
Enemy and obstacle behaviors are implemented using a modular state machine architecture designed for scalability and reusability.

Core states:
- Spawn / Initialization
- Active Behavior State
- Interaction / Collision Response
- Deactivation / Reset (for pooling compatibility)

This system allows easy extension of enemy and obstacle logic without modifying core gameplay flow.

---

### 🎯 Game Feel & Feedback System (Juice System)
The game emphasizes “juice-driven” feedback to improve player experience:

- Dynamic camera response based on speed and gameplay state  
- Particle-based speed and impact feedback  
- Motion reinforcement during transitions (acceleration / deceleration)  
- Visual synchronization between gameplay events and feedback systems  

---

### 📉 Death & Impact System
Player death is designed as a feedback-driven system rather than an instant state change:

- Curved deceleration system after collision  
- Delayed transition to game-over state for impact clarity  
- Enhanced visual feedback during fall / crash sequence  
- Controlled time pacing for better emotional response  

---

## 🧠 Technical Focus

- State Machine Architecture (scalable AI system design)  
- Object Pooling (performance optimization for mobile runtime)  
- Game Feel Engineering (camera, VFX, timing systems)  
- Mobile-first optimization for high responsiveness  

---

## 🛠️ Technologies

- Unity  
- C#  
- State Machine Design Pattern  
- Object Pooling System  
- Custom Game Feel Systems  

---

## 📌 Project Status

**In Development**

Actively being improved with additional systems, optimization, and gameplay polish.
