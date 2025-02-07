import React, { useEffect, useState } from 'react';
import service from './service.js';

function App() {
  
  const [newTodo, setNewTodo] = useState("");
  const [todos, setTodos] = useState([]);

  async function getTodos() {
    const todos = await service.getTasks();
    setTodos(todos);
  }

  async function createTodo(e) {
    e.preventDefault();
    await service.addTask(newTodo);
    setNewTodo("");//clear input
    await getTodos();//refresh tasks list (in order to see the new one)
  }

  async function updateCompleted(todo, IsComplete) {
    console.log(`update ${todo.idItems}`);

    
    await service.setCompleted(todo.idItems, IsComplete);
    await getTodos();//refresh tasks list (in order to see the updated one)
    
  }

  async function deleteTodo(idItems) {
    console.log("delete ");
    
    console.log(idItems);
    
    await service.deleteTask(idItems);
    await getTodos();//refresh tasks list
  }

  useEffect(() => {
    getTodos();
    
  }, []);

  return (
    <section className="todoapp">
      <header className="header">
        <h1>todos</h1>
        <form onSubmit={createTodo}>
          <input className="new-todo" placeholder="Well, let's take on the day" value={newTodo} onChange={(e) => setNewTodo(e.target.value)} />
        </form>
      </header>
      <section className="main" style={{ display: "block" }}>
        <ul className="todo-list">
          {todos.map(todo => { 
            return (
              <li className={todo.IsComplete ? "completed" : ""} key={todo.idItems}>
                <div className="view">
                  <input className="toggle" type="checkbox" defaultChecked={todo.IsComplete} onChange={(e) => updateCompleted(todo, e.target.checked)} />
                  <label>{todo.name}</label>
                  <button className="destroy" onClick={() => deleteTodo(todo.idItems)}></button>
                </div>
              </li>
            );
          })}
        </ul>
      </section>
    </section >
  );
}

export default App;