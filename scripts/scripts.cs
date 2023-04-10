// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using System.Collections.Generic;
// 创建Graph，邻接表

class Slot<T, U>{
    public int m_ID{get; set;}
    private T m_slot{get; set;}
    public Level<T, U> m_level{get; set;}
    public Type<U>? m_type{get; set;}
    public HashSet<Slot<T,U>> m_adj; // slots that connected with
    public List<Type<U>> m_possibleType {get; set;}

    public Slot<T, U>? left{get; set;}
    public Slot<T, U>? right{get; set;}
    public Slot<T, U>? up{get; set;}
    public Slot<T, U>? down{get; set;}
    public Slot<T, U>? back{get; set;}
    public Slot<T, U>? forward{get; set;}

    public Slot(int id, T slot, List<Type<U>> possibleType, Level<T, U> level){
        m_ID = id;
        m_slot = slot;
        m_adj = new HashSet<Slot<T, U>>();
        m_possibleType = possibleType;
        m_level = level;
        m_type = null;
        left = null;
        right = null;
        up = null;
        down = null;
    }

    public Slot(int id, T slot, List<Type<U>> possibleType, Level<T, U> level, Type<U> type){
        m_ID = id;
        m_slot = slot;
        m_adj = new HashSet<Slot<T, U>>();
        m_possibleType = possibleType;
        m_level = level;
        m_type = type;
    }
    public bool AddEdge(Slot<T, U> u){
        return m_adj.Add(u);
    }

}
struct UType<U>{
    public int m_ID{get; set;}
    public U m_value{get; set;}
    Type(int id, U value){
        m_ID = id;
        m_value = value;
    }
}
struct Edge<T, U>{
        public Slot<T, U> m_u{get; set;}
        public Slot<T, U> m_v{get; set;}
        public int m_weight{get; set;}
        public Edge(Slot<T, U> u, Slot<T, U> v, int weight){
        m_u = u;
        m_v = v;
        m_weight = weight;
        u.AddEdge(v);
        v.AddEdge(u);
        }
    }
class Graph<T, U>{
    
    private Dictionary<int, Slot<T, U>> m_slots;
    private List<Edge<T, U>> m_edges;
    public Graph(){
        m_slots = new Dictionary<int, Slot<T, U>>();
        m_edges = new List<Edge<T, U>>();
    }

    public void AddVertex(int id, T value, List<Type<U>> possibleType,int level){
        if(!m_slots.ContainsKey(id) )m_slots.Add(id, new Slot<T, U>(id, value, possibleType, level));
    }
    public void AddEdge(int u, int v, int weight){
        //AddVertex(u);
        //AddVertex(v);
        m_edges.Add(new Edge<T, U>(m_slots[u], m_slots[v], weight));
    }
    public void PrintGraph(){
        Console.Write("HI");
        foreach(Slot<T, U> v in m_slots.Values){
            Console.Write("Vertex: " + v.m_ID + " connecte with ");
            foreach(Slot<T, U> u in v.m_adj){
                Console.Write(u.m_ID + " ");
            }
        }
    }
}
//
class SLevel<T, U>{
    public int m_ID{get; set;}
    public Graph<T, U> m_graph{get; set;}
    public Rule<U> m_rule{get; set;}

    public List<Type<U>> m_types{get; set;}
    public Level<T, U>? m_up{get; set;}
    public Level<T, U>? m_down{get; set;}
    public Level(int id, Graph<T, U> graph, List<Type<U>> types, Rule<U> rule, Level<T, U> up, Level<T, U> down){
        m_ID = id;
        m_graph = graph;
        m_up = up;
        m_down = down;
        m_rule = rule;
        m_types = types;
    }

}
//class Rule<U>{
    public int m_ID{get; set;}
    public int m_level{get; set;}
    public Dictionary<Type<U>, HashSet<Type<U>>> m_dict;
    Rule(int id, int level){
        m_ID = id;
        m_level = level;
        m_dict = new Dictionary<Type<U>, HashSet<Type<U>>>();
    }
    public void AddRule(Type<U> type, HashSet<Type<U>> possibleType){
        m_dict.Add(type, possibleType);
    }
}

class WaveFunctionCollapse<T, U>{
    private Graph<T, U> m_slots;
    private List<Level<T, U>> m_levels;
    private Random m_rd;
    public WaveFunctionCollapse(List<Level<T, U>> levels){
        m_slots = new Graph<T, U>();
        m_levels = levels;
        m_rd = new Random();
    }

    public bool Observe(Slot<T, U> slot){
        if(slot.m_type != null) return false;
        Type<U> type = slot.m_possibleType[m_rd.Next(0, slot.m_possibleType.Count)];
        slot.m_type = type;
        slot.m_possibleType.Clear();  // clear possibility
        foreach(Slot<T, U> adjSlot in slot.m_adj){
            Propagation(type, adjSlot);
        }
        return true;
    }

    public void Propagation(Type<U> pType, Slot<T, U> slot){  // re-adjust possibility
        HashSet<Type<U>> possibleTypes = slot.m_level.m_rule.m_dict[pType];
        List<Type<U>> levelTypes = slot.m_level.m_types;
        
        foreach(Type<U> type in levelTypes){
            if(!possibleTypes.Contains(type) && slot.m_possibleType.Contains(type)){
                slot.m_possibleType.Remove(type);
            }
        }
    }

    public void Generate(Slot<T, U> slot){
        Slot<T, U> left = slot.left;
        Slot<T, U> right = slot.right;
        Slot<T, U> forward = slot.forward;
        Slot<T, U> back = slot.back;
        Slot<T, U> up = slot.up;
        Slot<T, U> down = slot.down;
        //[TOP, DOWN, LEFT, RIGHT, FORWARD, BACK]
        int code = 0;
        if(left != null)    code |= 0B_0001;     //0001
        if(right != null)   code |= 0B_0010;     //0010
        if(up != null)      code |= 0B_0100;     //0100
        down = null;
        if(down != null)    code |= 0B_1000;     //1000

        if(up == null) CreateTop();
        if(down == null) CreateBottom();

        switch(code){
            case 0B_0000:
                break;

            case 0B_0001:
                break;
            case 0B_0010:
                break;
            case 0B_0100:
                break;
            case 0B_1000:
                break;

            case 0B_0011:
                break;
            case 0B_1100:
                break;
            case 0B_0110:
                break;
            case 0B_1001:
                break;
            case 0B_1010:
                break;
            case 0B_0101:
                break;

            case 0B_1110:
                break;
            case 0B_1101:
                break;
            case 0B_1011:
                break;
            case 0B_0111:
                break;

            case 0B_1111:
                break;
        }
    }

    public void CreateTop(){

    }
    public void CreateBottom(){

    }

}


class Grid<P, T>{
    public P point;
    public int id;
    //public int degree;
    public Possibility<T> choices;
    public Type<T>? type{get; set;}
    public Grid(int ID, P pt, Possibility<T> choice){
        id = ID;
        point = pt;
        choices = choice;
        type = null;
        //adj = new List<Grid<P, T>>();
    }


    public void SetType(Random random){
        type = choices.GetType(random);
    }
    public bool hasType(){
        return type != null;
    }
}

class Level<P, T>{  // 每个level有一个平面图
    public int level;
    public Dictionary<int, Unit<P, T>> units;
    public Rule<T> rule;
    public Level<P, T> up {get; set;}
    public Level<P, T> down {get; set;}
    //public Possibility<T> choices;
    public Level(int levelID){
        level = levelID;
        units = new Dictionary<int, Unit<P, T>>();
        rule = new Rule<T>();
        up = null;
        down = null;
    }
}
class Rule<T>{  // 每个level有一条rule
    public Dictionary<Type<T>, HashSet<Type<T>>> rules;
    public Rule(){
        rules = new Dictionary<Type<T>, HashSet<Type<T>>>();
    }
    public bool AddRule(Type<T> type, HashSet<Type<T>> adjType){
        if(rules.ContainsKey(type)) return false;
        rules.Add(type, adjType);
        return true;
    }
    public bool ClearRule(Type<T> type){
        return rules.Remove(type);
    }
}
class Block<P, T> : Grid<P, T>{
    //public Corner<P, T>[] coners;

    public Block(int ID, P pt, Possibility<T> choice) : base(ID, pt, choice)
    {
        //coners = new Corner<P, T>[this.GetDegree() * 2];
    }

} 
class Corner<P, T> : Grid<P, T>{
    public Corner(int ID, P pt, Possibility<T> choice) : base(ID, pt, choice){

    }
}

class Unit<P, T>{
    public Block<P, T> block;
    public Corner<P, T>[] corners;
    public List<Unit<P, T>> adj;

    public Level<P, T> level;
    public Unit<P, T> up{get; set;}
    public Unit<P, T> down{get; set;}
    public Unit(Block<P, T> core, Level<P, T> lvl){
        corners = new Corner<P, T>[this.GetDegree()];
        level = lvl;
    }
    public Block<P, T> GetBlock(){
        return block;
    }
    public Corner<P, T>[] GetCorners(){
        return corners;
    }
    public Corner<P, T> GetCorner(int id){
        return corners[id];
    }
    public int GetDegree(){
        return adj.Count;
    }

    public Level<P, T> GetLevel(){
        return level;
    }
}
class Layer<P, T>{
    public Level<P, T> level;
    public List<Unit<P, T>> units;

}

class Type<T>{
    public int ID;
    public T mesh;
    Type(int id, T ms){
        ID = id;
        mesh = ms;
    }
    

}
class Possibility<T>{ // 每个Grid会带一个Prossibility
    public List<Type<T>> types{set; get;} // dictionary?
    public Possibility(){
        types = new List<Type<T>>();
    }
    public void Add(Type<T> type, int times){
        for(int i = 0; i < times; i++){
            types.Add(type);
        }
    }
    public void Remove(Type<T> type){
        types.Remove(type);
    }
    public void RemoveAll(Type<T> type){
        types.RemoveAll(data => type == data );
    }

    public Type<T> GetType(Random random){
        return types[random.Next(types.Count)];
    }
}


class World<P, T>{
    private List<Level<P, T>> levels;
    private Random random;
    public World(){
        levels = new List<Level<P, T>>();
        random = new Random();
    }

    public bool Collapse(Unit<P, T> unit, int depth){
        if(depth == 0) return false;
        Block<P, T> block = unit.GetBlock();
        if(!block.hasType()) return false;
        block.SetType(random);

        RegulateUp(block.type, unit.up);
        RegulateDown(block.type, unit.down);
        foreach(Unit<P, T> relative in unit.adj){
            RegulateRelative(block.type, relative);
        }
        foreach(Unit<P, T> relative in unit.up.adj){
            RegulateUpRelative(block.type, relative);
        }
        foreach(Unit<P, T> relative in unit.down.adj){
            RegulateDownRelative(block.type, relative);
        }
        Collapse(adj, depth - 1);
        return true;
    }


    public void Observe(Unit<P, T> unit, int depth){
        Queue<Unit<P, T>> waitingToCollapse = new Queue<Unit<P, T>>();
        waitingToCollapse.Enqueue(unit);
        int levelSize = waitingToCollapse.Count;
        while(depth != 0 && waitingToCollapse.Count != 0){
            for(int i = 0; i < levelSize; i++){
                Unit<P, T> curUnit = waitingToCollapse.Dequeue();
                Collapse(curUnit);
                Regulate(curUnit);
                foreach(Unit<P, T> relative in curUnit.adj){
                    waitingToCollapse.Enqueue(relative);
                }
            }
            depth--;
        }
        
    }
    public bool Collapse(Unit<P, T> unit){
        Block<P, T> block = unit.GetBlock();
        if(!block.hasType()) return false;
        block.SetType(random);
        //Collapse(adj, depth - 1);
        return true;
    }






    private void Regulate(Type<T> pType, Unit<P, T> unit){
        Block<P, T> block = unit.GetBlock();
        RegulateUp(block.type, unit.up);
        RegulateDown(block.type, unit.down);
        foreach(Unit<P, T> relative in unit.adj){
            RegulateRelative(block.type, relative);
        }
        foreach(Unit<P, T> relative in unit.up.adj){
            RegulateUpRelative(block.type, relative);
        }
        foreach(Unit<P, T> relative in unit.down.adj){
            RegulateDownRelative(block.type, relative);
        }
    }
    private void RegulateRelative(Type<T> pType, Unit<P, T> unit){

    }
    private void RegulateUpRelative(Type<T> pType, Unit<P, T> unit){
        
    }
    private void RegulateDownRelative(Type<T> pType, Unit<P, T> unit){
        
    }
    private void RegulateUp(Type<T> pType, Unit<P, T> unit){
        
    }
    private void RegulateDown(Type<T> pType, Unit<P, T> unit){
        
    }
}