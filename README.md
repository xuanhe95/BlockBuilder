# 🌆 GenericBuilder 🏙

**Generic Builder**是一款基于Unity的游戏。  
我们来自 🏫 UCLA，这个游戏是我们Studio成果的一部分。🐻Go Bruins！   
We are from 🏫 UCLA and this program is one of our studio works. 🐻Go Bruins! 

☕️🧑‍💻 Program Design:    **Xander Zhang**  
🎨🧑‍🎨 Technical Art:     **Ishmeal Luo** 

---

## ♟️ 基本功能：  

- **鼠标左键**：🖱️ 点击方块可以根据 🔗 连接规则生成新*方块*。  
- **鼠标右键**：🖱️ 点击任意位置可以执行 🔙 回退操作。  
- **按X并鼠标左键**：⌨️ “X“按下时 🖱️ 点击方块可以按规则 ♻️ 更新范围距离内的*方块*。  
- **鼠标移动**：🖱️ 移动到可生成方块的位置时，🎲 随机生成合法的*方块*。
- **WASD按键**：⌨️ “W/A/S/D”按下时可以控制📹游戏镜头角度。

## ♟️ Game Function:

- **Left mouse button**: 🖱️ By clicking on the block, a new block can be created according to 🔗 the rules. 
- **Right mouse button**: 🖱️ A click anywhere enables 🔙 the undo operation. 
- **Using "X" key with the left mouse button**: When ⌨️ the "X" key is pressed, 🖱️ clicking on the block allows for ♻️ updating blocks within a specified range. 
- **Mouse movement**: When 🖱️ the mouse is moved to a position where a block can be generated, 
a preview of random selectable block will be created.
- **WASD keys**: The game's camera angle can be adjusted by pressing ⌨️ the "W/A/S/D" keys. 

---

## ✨ 程序设计：  

- 📦 采用泛型来封装游戏数据类型，以确保在不同的库（如Unity，Rhino等）下提高代码的扩展性。 
- 🌲 利用面向对象设计管理游戏数据，使游戏的层级逻辑更为清晰和易理解。
- ☯️ 将用于数据管理的类型（Generic）与用于操作数据的类型（GroupManager）进行区分，以便于代码的维护和管理。
- 🧑‍🎨 游戏设计师可通过Unity的图形用户界面来自由地建立方块间的规则。

## ✨ Program Design:

- 📦 We use generics to encapsulate game data types, enhancing code extensibility across various libraries like Unity, Rhino, and others.
- 🌲 By utilizing object-oriented design to manage game data, we ensure that the game's hierarchical logic is more clear and understandable.
- ☯️ By segregating the types used for data management (Generic) from those used to manipulate data (GroupManager), we facilitate easier code maintenance.
- 🧑‍🎨 Game designers have the freedom to establish rules between blocks via Unity's Graphical User Interface.

---

## 📁 Generic下的类：
这些类被用于管理游戏对象的数据。 

### 📃 Group <P, T>
Group类用于存储方块的GameObject模型，并维护管理方块的Type和Choice类。  
此外，GroupHelper被用于实现一些与Group类相关的方法。

Group类包含：
   - Unit列表，用于存储组成当前方块的GameObjects。
   - Choice类，用于存储当前Block的可选Type。
   - Type类，用于标识Block的Type。 

#### Group Helper

- GetAdjacnetGroup方法：获得Group对应方向的相邻Group。
- GetLevel方法：获得Group对应的Level。

### 📃 Unit <P, T> 
Unit类是组成模型的最小单元，这是为了方便未来实现*更自由的网格形状*而预留的接口。

### 📃 Level <P, T>
Level类用于记录每一层级的信息。
Level类包含：

- 用于生成地图网格的长度和宽度，以及每个Level的高度，这些信息用于指导游戏模型的生成。
- Rule类，用于指导每层Level的方块生成规则。
- Groups和Units字典，用于管理每一层的Groups和Units。
- Up和Down，用于获取每个Level的上一个或下一个Level。

### 📃 Type <T\> 
 
- Type类记录了一个泛型<T> Parent和一组List<T> Types，用于记录方块的当前类型信息。
- 在调用instantiator时，游戏程序将根据Types内的GameObject实例化对应的游戏模型。
 
### 📃 Rule <T\>  

- Rule类用于记录并管理Block间的连接关系。
- Rule类包含一个Type类型的字典，用于记录Type与其他Type之间的连接关系。

### 📃 Choice <T\>
 
- Choice类记录了一个Type列表，用于保存可选择的类型。

---

## 📁 GroupManager类   

GroupManager类被用来维护方块之间的动态关系，并生成每个位置的可选方块列表。  
为了保持代码的可读性，GroupManager被分为了几个部分：

### 📃 GroupManager 

GroupManager类包含：  
- Group类型，记录被管理的Group。  
- GeoMap字典，用来建立Geo和Type之间的数据关系。
- ModMap字典，用来建立GameObject和Type之间的数据关系。这个字典用于获取临近的Group。
   （Group类的GetRelatives方法只能获取GameObject，所以需要这个字典来记录对应的关系）。 

### 📃 GroupNext

GroupNext负责获取当前Group的可选Type列表，这个功能用于实现预览功能。

### 📃 GroupHelper
   
GroupHelper提供了一些🔧工具方法，用来判断当前相同类型方块形成的空间状态关系，以此来实现更复杂的交互逻辑规则。  
具体方法包括：

   - L：检查当前生成位置的两个对角是否为同一类型的方块，并返回一个方向。
   - H：检查当前生成位置的两个相对边是否为同一类型的方块，并返回一个方向。
   - C：检查当前生成位置的三个边是否为同一类型的方块，并返回一个方向。
   - O：检查当前生成位置是否被四个相同类型的方块所包围。

---

## 📁 Generator类：  

### 📃 Generator
为了保持代码的可读性，Generator类被划分为多个部分：
### 📃 Instantiator
- 此部分用于实例化需要显示的GameObject类。
### 📃 GroupGenerator
- 此部分被用来生成并组织Group和Unit类型。
### 📃 LevelGenerator
- 此部分被用来生成并组织Level类型。
### 📃 RuleGenerator
- 此部分被用来生成Rule类型。
### 📃 ChoiceGenerator
- 此部分被用来生成Choice类型。
### 📃 CursorManager
- 此部分用于实现Block六个面的交互。
### 📃 GeneratorHelper
- 一些工具类，用于辅助Unity GameObject的创建。
### 📃 Preview
- 此部分通过记录和维护当前可选的GameObject列表，来实现方块的预览。
### 📃 History
- 此部分使用栈来保存访问过的Group，从而实现游戏历史和回退功能。

---

## 📁 Helper下的类：
### 📃 RuleCreator

- RuleCreator是一个用于指定Block连接关系的工具，在Unity中公开。它允许用户通过Unity的UI界面来编写连接关系。
- RuleGenerator将读取RuleCreator中的数据，以此为模版创建Rules。

### 📃 GeoPicker
- GeoPicker负责读取Prefab并返回生成的GameObject列表。

### 📃 Direction
- Direction是一个静态类，用于记录方向。

### 📃 Geo
- Geo是一个枚举类型，用于记录模型的类型。
