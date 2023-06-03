# GenericBuilder

**Generic Builder**是一款基于Unity的游戏。  
我们来自UCLA，这个游戏是我们Studio成果的一部分。🐻Go Bruins！   
We are from UCLA and this program is one of our studio works. 🐻Go Bruins! 

*Program by Xander Zhang*  
*Technical Art by Ishmeal Luo*

---

## 基本功能：  

- **鼠标左键**：点击方块可以根据链接规则生成新*方块*。  
- **鼠标右键**：点击任意位置可以执行回退操作。  
- **按X并鼠标左键**：点击方块可以按规则更新范围内的*方块*。  
- **鼠标移动到可生成方块的位置**：生成随机可选择*方块*。

**Generic Builder** is a Unity game.  
Left-clicking on each block allows you to generate new blocks based on predefined rules.  
Right-clicking allows you to undo the previous step.  
Pressing X and left-clicking allows you to regenerate blocks within a certain range.  

---

## 设计亮点：  

- 使用泛型对游戏数据类型进行包装，以保证代码在不同库下（Unity，Rhino等）有更好的扩展性。  
- 通过面向对象设计来管理组织游戏数据，使游戏层级逻辑更清晰。 
- 将用来进行数据管理的类型（Generic）和用来操作数据的类型（GroupManager）相分隔，使代码更易维护。  
- RuleCreator类方便游戏设计师通过Unity的GUI来建立方块之间的规则。 

---

## Generic文件夹下的类：
 用于对游戏物体数据进行管理的类。  

### Group
 用来显示方块，维护管理方块类型及选择的类  

Group类中有：
 - Unit列表，用来储存组成当前方块的GameObjects。 
 - Choice类，用来存放当前位置的可选项。  
 - Type类，用来标识方块的类型。 

### Unit  
用来组成模型的最小单元，为了方便未来实现更自由的网格形态而留下的stub。 

### Level
用来记录每一个层级的信息。 
Level类中有： 
- 生成地图网格的长度与宽度，以及每个Level的高度，用以指导模型生成。 
- Rule类，用来指导每个Level的方块生成关系。 
- Groups及Units字典，用以管理每一层的Groups及Units。  
- Up以及Down，用来获得每个Level的上一个Level或下一个Level。 

### Rule  
用来记录并管理方块间的连接关系。  
Rule类型中包含：  
- Type类型的字典，用来记录Type和Type之间的连接关系。 

---

## GroupManager类   

用来维护方块之间的动态关系，生成每个位置的可选择方块列表。 
为了保持代码功能的清晰，GroupManager被分为了几个部分：   

### GroupManager 

GroupManager类中有：  
- Group类型，记录被管理的Group。  
- GeoMap字典，建立Geo和Type之间的数据关系。 
- ModMap字典，建立GameObject和Type之间的数据关系，用来获得临近的Group。（Group中的GetRelatives方法只能获得GameObject，因此需要此方法记录对应关系） 

### GroupNext

获取当前Group的可选Type列表，用来实现Preview功能。 

### GroupHelper
- 一些工具方法用来判断当前同类型方格形成的空间状态关系，以此实现更复杂的交互逻辑规则。 
- L：检测当前生成位置的两个对角是否是同样类型的方块，并返回一个方向。  
- H：检测当前生成位置的两个相对边是否是同样类型的方块，并返回一个方向。  
- C：检测当前生成位置的三个边是否是同样的类型方块，并返回一个方向。  
- O：检测当前生成位置是否被四个同样的类型方块所包围。 

---

## Generator类：  

### Generator
为了保持代码功能的清晰，Generator被分为了几个部分：
### Instantiator
- 实例化需要显示的GameObject类。
### GroupGenerator
- 用以生成并组织Group与Unit类型。
### LevelGenerator
- 用以生成并组织Level类型。
### RuleGenerator
- 用以生成Rule类型。
### ChoiceGenerator
- 用以生成Choice类型。
### CursorManager
- 实现方块的六个面的交互。
### GeneratorHelper
- 一些工具类，用以辅助Unity GameObject的创建。
### Preview
- 通过记录并维护当前可选择的GameObject列表来实现方块的预览。
### History
- 使用栈保存访问过的Group以实现游戏历史及回退功能。

---

## Helper文件夹下的类：
### RuleCreator

用于指定方块连接关系的工具，在Unity中暴露。使得可以通过Unity的UI界面编写连接关系。 

### GeoPicker
读取Prefab并返回生成的GameObject列表。 

### Direction
静态类，用于记录方向。 

### Geo
枚举，用于记录模型的类型。 
