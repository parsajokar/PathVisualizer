# 🧭 Pathfinding Visualizer

A **WPF (C#)** desktop application that visualizes **Depth-First Search (DFS)** and **Breadth-First Search (BFS)** on a **20×20 grid**.

![DFS Demo](demo/dfs_demo.mp4)
![BFS Demo](demo/bfs_demo.mp4)

## 🛠️ Technologies Used

* **C#**
* **WPF**
* **.NET**
* **XAML**

## 📐 Grid Behavior

* The grid consists of **400 cells (20×20)**.
* Each cell can be:
  * **Empty** – traversable
  * **Wall** – not traversable
* Clicking a cell toggles its state.

## 🧠 Algorithms Implemented

### Depth-First Search (DFS)

* Implemented with System.Collections.Stack instead of using recursive functions for optimization purposes.
* Does **not** guarantee shortest path.

### Breadth-First Search (BFS)

* Used System.Collections.Queue as the main data structure.
* Guarantees shortest path in an unweighted grid.

## 🎮 How to Use

1. Launch the application.
2. Click on grid cells to add or remove **walls**.
3. Choose an algorithm (**BFS** or **DFS**).

## 🚀 Getting Started

1. Clone the repository:

   ```bash
   git clone https://github.com/parsajokar/PathVisualizer.git
   ```

2. Open the solution in **Visual Studio** or **JetBrains Rider**
3. Build and run the project
