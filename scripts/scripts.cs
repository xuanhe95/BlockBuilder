// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using System.Collections.Generic;


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

class Unit<P, T>{
    public P point;
    public Type type;
    public Possibility<T> choices;
    public Type<T>? type { get; set; }
    public int id;
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

    public void SetType(Random random)
    {
        type = choices.GetType(random);
    }
    public bool hasType()
    {
        return type != null;
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