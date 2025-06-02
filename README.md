# Run dotnet Application
Open Online App.sln and run application

# React-OnlineShop
Create Online Shop using React

# Install node_module
go to client folder and run npm install

# Run Client App
npm run dev

# UI Libray Used for development
npm install @mui/material @emotion/react @emotion/styled

# Install Redux Toolkit and React-Redux
npm install @reduxjs/toolkit react-redux

# Install redux router dom
npm install react-router-dom

# React Tutorial

React is a JavaScript library for building user interfaces.
React is used to build single-page applications.
React allows us to create reusable UI components.

How does React Work?

React creates a VIRTUAL DOM in memory.
Instead of manipulating the browser's DOM directly, React creates a virtual DOM in memory, where it does all the necessary manipulating, 
before making the changes in the browser DOM.

React.JS History
Latest version of React.JS is 19.0.0 (December 2024).
Initial release to the Public (version 0.3.0) was in July 2013.
React.JS was first used in 2011 for Facebook's Newsfeed feature.
Facebook Software Engineer, Jordan Walke, created it.

As of June 2025, the latest stable version of React is 19.1.0, released in March 2025.

What is JSX?
JSX stands for JavaScript XML.
JSX allows us to write HTML in React.
JSX makes it easier to write and add HTML in React.

Without JSX:

const myElement = React.createElement('h1', {}, 'I do not use JSX!');
const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(myElement);

With JSX:

const myElement = <h1>I Love JSX!</h1>;
const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(myElement);

# React Components

Components are independent and reusable bits of code. They serve the same purpose as JavaScript functions, but work in isolation and return HTML.
Components come in two types, Class components and Function components.

# Class Component
A class component must include the extends React.Component statement. This statement creates an inheritance to React.Component, and gives your component access to React.Component's functions.
The component also requires a render() method, this method returns HTML.

class Car extends React.Component {
  render() {
    return <h2>Hi, I am a Car!</h2>;
  }
}

# Function Component

A Function component also returns HTML, and behaves much the same way as a Class component, 
but Function components can be written using much less code, are easier to understand

function Car() {
  return <h2>Hi, I am a Car!</h2>;
}

# Props

Components can be passed as props, which stands for properties.
Props are like function arguments, and you send them into the component as attributes.

function Car(props) {
  return <h2>I am a {props.color} Car!</h2>;
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<Car color="red"/>);

# React Forms

You add a form with React like any other element:

import { useState } from 'react';
import ReactDOM from 'react-dom/client';

function MyForm() {
  const [inputs, setInputs] = useState({});

  const handleChange = (event) => {
    const name = event.target.name;
    const value = event.target.value;
    setInputs(values => ({...values, [name]: value}))
  }

  const handleSubmit = (event) => {
    event.preventDefault();
    alert(inputs);
  }

  return (
    <form onSubmit={handleSubmit}>
      <label>Enter your name:
      <input 
        type="text" 
        name="username" 
        value={inputs.username || ""} 
        onChange={handleChange}
      />
      </label>
      <label>Enter your age:
        <input 
          type="number" 
          name="age" 
          value={inputs.age || ""} 
          onChange={handleChange}
        />
        </label>
        <input type="submit" />
    </form>
  )
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<MyForm />);

# React Router

npm i -D react-router-dom

import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "./pages/Layout";
import Home from "./pages/Home";
import Blogs from "./pages/Blogs";
import Contact from "./pages/Contact";
import NoPage from "./pages/NoPage";

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Home />} />
          <Route path="blogs" element={<Blogs />} />
          <Route path="contact" element={<Contact />} />
          <Route path="*" element={<NoPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<App />);

import { Outlet, Link } from "react-router-dom";

const Layout = () => {
  return (
    <>
      <nav>
        <ul>
          <li>
            <Link to="/">Home</Link>
          </li>
          <li>
            <Link to="/blogs">Blogs</Link>
          </li>
          <li>
            <Link to="/contact">Contact</Link>
          </li>
        </ul>
      </nav>

      <Outlet />
    </>
  )
};

export default Layout;

note:-JavaScript expressions are written inside curly braces, and since JavaScript objects also use curly braces, the styling in the example above is written inside two sets of curly braces {{}}

# React Hooks

Hooks were added to React in version 16.8
Hooks allow function components to have access to state and other React features. 

What is a Hook?
Hooks allow us to "hook" into React features such as state and lifecycle methods.

Hook Rules
There are 3 rules for hooks:-
1.Hooks can only be called inside React function components.
2.Hooks can only be called at the top level of a component.
3.Hooks cannot be conditional

useState 
The React useState Hook allows us to track state in a function component.
State generally refers to data or properties that need to be tracking in an application.

import { useState } from "react";
import ReactDOM from "react-dom/client";

function FavoriteColor() {
  const [color, setColor] = useState("red");

  return (
    <>
      <h1>My favorite color is {color}!</h1>
      <button
        type="button"
        onClick={() => setColor("blue")}
      >Blue</button>
    </>
  )
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<FavoriteColor />);

import { useState } from "react";
import ReactDOM from "react-dom/client";

function Car() {
  const [car, setCar] = useState({
    brand: "Ford",
    model: "Mustang",
    year: "1964",
    color: "red"
  });

  const updateColor = () => {
    setCar(previousState => {
      return { ...previousState, color: "blue" }
    });
  }

  return (
    <>
      <h1>My {car.brand}</h1>
      <p>
        It is a {car.color} {car.model} from {car.year}.
      </p>
      <button
        type="button"
        onClick={updateColor}
      >Blue</button>
    </>
  )
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<Car />);

useEffect 

The useEffect Hook allows you to perform side effects in your components.
Some examples of side effects are: fetching data, directly updating the DOM, and timers.
useEffect accepts two arguments. The second argument is optional.

import { useState, useEffect } from "react";
import ReactDOM from "react-dom/client";

function Timer() {
  const [count, setCount] = useState(0);

  useEffect(() => {
    let timer = setTimeout(() => {
    setCount((count) => count + 1);
  }, 1000);

  return () => clearTimeout(timer)
  }, []);

  return <h1>I've rendered {count} times!</h1>;
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<Timer />);

useContext

React Context is a way to manage state globally.
It can be used together with the useState Hook to share state between deeply nested components more easily than with useState alone.

import { useState, createContext, useContext } from "react";
import ReactDOM from "react-dom/client";

const UserContext = createContext();

function Component1() {
  const [user, setUser] = useState("Jesse Hall");

  return (
    <UserContext.Provider value={user}>
      <h1>{`Hello ${user}!`}</h1>
      <Component2 />
    </UserContext.Provider>
  );
}

function Component2() {
  return (
    <>
      <h1>Component 2</h1>
      <Component3 />
    </>
  );
}

function Component3() {
  return (
    <>
      <h1>Component 3</h1>
      <Component4 />
    </>
  );
}

function Component4() {
  return (
    <>
      <h1>Component 4</h1>
      <Component5 />
    </>
  );
}

function Component5() {
  const user = useContext(UserContext);

  return (
    <>
      <h1>Component 5</h1>
      <h2>{`Hello ${user} again!`}</h2>
    </>
  );
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<Component1 />);

useRef 

The useRef Hook allows you to persist values between renders.
It can be used to store a mutable value that does not cause a re-render when updated.
It can be used to access a DOM element directly.

import { useState, useEffect, useRef } from "react";
import ReactDOM from "react-dom/client";

function App() {
  const [inputValue, setInputValue] = useState("");
  const count = useRef(0);

  useEffect(() => {
    count.current = count.current + 1;
  });
return (
    <>
      <input
        type="text"
        value={inputValue}
        onChange={(e) => setInputValue(e.target.value)}
      />
      <h1>Render Count: {count.current}</h1>
    </>
  );
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<App />);

Tracking State Changes
The useRef Hook can also be used to keep track of previous state values.

import { useState, useEffect, useRef } from "react";
import ReactDOM from "react-dom/client";

function App() {
  const [inputValue, setInputValue] = useState("");
  const previousInputValue = useRef("");

  useEffect(() => {
    previousInputValue.current = inputValue;
  }, [inputValue]);

  return (
    <>
      <input
        type="text"
        value={inputValue}
        onChange={(e) => setInputValue(e.target.value)}
      />
      <h2>Current Value: {inputValue}</h2>
      <h2>Previous Value: {previousInputValue.current}</h2>
    </>
  );
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<App />);

useReducer 

The useReducer Hook is similar to the useState Hook.
It allows for custom state logic.
If you find yourself keeping track of multiple pieces of state that rely on complex logic, useReducer may be useful.

The useReducer Hook accepts two arguments.
useReducer(<reducer>, <initialState>)

The reducer function contains your custom state logic and the initialStatecan be a simple value but generally will contain an object.
The useReducer Hook returns the current stateand a dispatchmethod.

import { useReducer } from "react";
import ReactDOM from "react-dom/client";

const initialTodos = [
  {
    id: 1,
    title: "Todo 1",
    complete: false,
  },
  {
    id: 2,
    title: "Todo 2",
    complete: false,
  },
];

const reducer = (state, action) => {
  switch (action.type) {
    case "COMPLETE":
      return state.map((todo) => {
        if (todo.id === action.id) {
          return { ...todo, complete: !todo.complete };
        } else {
          return todo;
        }
      });
    default:
      return state;
  }
};

function Todos() {
  const [todos, dispatch] = useReducer(reducer, initialTodos);

  const handleComplete = (todo) => {
    dispatch({ type: "COMPLETE", id: todo.id });
  };

  return (
    <>
      {todos.map((todo) => (
        <div key={todo.id}>
          <label>
            <input
              type="checkbox"
              checked={todo.complete}
              onChange={() => handleComplete(todo)}
            />
            {todo.title}
          </label>
        </div>
      ))}
    </>
  );
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<Todos />);

Custom Hook

A custom hook is a JavaScript function whose name starts with use and can call other hooks (like useState, useEffect, etc.).

Create the custom hook
import { useState, useEffect } from "react";

const useFetch = (url) => {
  const [data, setData] = useState(null);

  useEffect(() => {
    fetch(url)
      .then((res) => res.json())
      .then((data) => setData(data));
  }, [url]);

  return [data];
};

export default useFetch;

Use it in a component
import ReactDOM from "react-dom/client";
import useFetch from "./useFetch";

const Home = () => {
  const [data] = useFetch("https://jsonplaceholder.typicode.com/todos");

  return (
    <>
      {data &&
        data.map((item) => {
          return <p key={item.id}>{item.title}</p>;
        })}
    </>
  );
};

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<Home />);

When to Use Custom Hooks?

Fetching or updating data
Handling timers/intervals
Forms or input handling
Reusing logic across multiple components

# Slice in react
A slice is a portion of the Redux state and all the logic (actions + reducers) related to that portion, bundled together in one place.

A slice includes:
A name – to identify this part of the state

Initial state – the default value

Reducers – functions to handle actions and update the state

Automatically generated action creators

Why is it called a "slice"?
Because it represents a slice of the global Redux state tree.

// features/counter/counterSlice.js
import { createSlice } from '@reduxjs/toolkit';

const counterSlice = createSlice({
  name: 'counter',
  initialState: { value: 0 },
  reducers: {
    increment(state) {
      state.value += 1;
    },
    decrement(state) {
      state.value -= 1;
    },
    incrementByAmount(state, action) {
      state.value += action.payload;
    },
  },
});

export const { increment, decrement, incrementByAmount } = counterSlice.actions;
export default counterSlice.reducer;

How to use in react

// App.js or Counter.js
import { useSelector, useDispatch } from 'react-redux';
import { increment } from './features/counter/counterSlice';

function Counter() {
  const count = useSelector(state => state.counter.value);
  const dispatch = useDispatch();

  return (
    <>
      <p>{count}</p>
      <button onClick={() => dispatch(increment())}>Increment</button>
    </>
  );
}

# useSelector

 What is useSelector in React?
useSelector is a React-Redux hook that allows your React component to read data from the Redux store.

const result = useSelector(selectorFunction);
selectorFunction: A function that takes the entire Redux state and returns the part of the state you want.

{
  counter: {
    value: 5
  }
}

import { useSelector } from 'react-redux';

function CounterDisplay() {
  const count = useSelector((state) => state.counter.value);

  return <h1>Count: {count}</h1>;
}

How it works:
It subscribes your component to the Redux store.

When the selected part of the state changes, the component automatically re-renders.

It's a replacement for mapStateToProps in older class-based components.

# useDispatch

What is useDispatch in React?
useDispatch is a hook provided by React-Redux that lets you dispatch actions to the Redux store from your React components.

import { useDispatch } from 'react-redux';

const dispatch = useDispatch();

You then call dispatch(action) to update the store

import { useDispatch } from 'react-redux';
import { increment } from './features/counter/counterSlice';

function CounterButton() {
  const dispatch = useDispatch();

  return (
    <button onClick={() => dispatch(increment())}>
      Increment
    </button>
  );
}

increment() is an action creator from a Redux slice.

dispatch() sends that action to the store.

The store runs the reducer and updates the state.

# useSelector vs useDispatch

Hook	        Purpose
useSelector	    Read data from Redux store
useDispatch	    Send actions to Redux store

# Store

What is a Store in React (Redux context)?
In a React + Redux application, the store is the central place where your entire app's state lives.

Think of the store as a single source of truth that holds all the data your app needs to function — like user info, UI state, and server responses.

What does the Store do?
Holds the application state

Allows reading the state via useSelector

Allows updating the state via dispatch(action)

Notifies components when state changes

// store.js
import { configureStore } from '@reduxjs/toolkit';
import counterReducer from './features/counter/counterSlice';

const store = configureStore({
  reducer: {
    counter: counterReducer,
  },
});

export default store;

Providing the Store
To use the store in your React app, wrap your root component in a <Provider>:

// index.js
import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import store from './store';
import App from './App';

ReactDOM.render(
  <Provider store={store}>
    <App />
  </Provider>,
  document.getElementById('root')
);

Component (UI)
   │
   ├─ useSelector() ←── Reads from ──┐
   ├─ useDispatch() ──→ Dispatches ─→│
   │                                 ↓
  Redux Store (state container) ←── Reducers

In Simple Terms:
Store = App’s brain 🧠 (holds state)

useSelector = Read from store

useDispatch = Send instructions to update store




====================================================================================================================================================================================================================================

# RTK Query Quick Start(React Toolkit Query)
RTK Query is a powerful data fetching and caching tool that is part of Redux Toolkit.
It simplifies handling remote data (API calls) in React apps.

Why use RTK Query?
Handles data fetching, caching, loading/error states automatically
Reduces boilerplate vs. useEffect + axios + Redux
Works seamlessly with Redux store

npm install @reduxjs/toolkit react-redux

// services/apiSlice.js
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

export const apiSlice = createApi({
  reducerPath: 'api', // where in Redux state this will be stored
  baseQuery: fetchBaseQuery({ baseUrl: 'https://jsonplaceholder.typicode.com/' }),
  endpoints: (builder) => ({
    getUsers: builder.query({
      query: () => 'users',
    }),
  }),
});

export const { useGetUsersQuery } = apiSlice;

Add it to the Redux store

// store.js
import { configureStore } from '@reduxjs/toolkit';
import { apiSlice } from './services/apiSlice';

export const store = configureStore({
  reducer: {
    [apiSlice.reducerPath]: apiSlice.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(apiSlice.middleware),
});

Use it in a component

// Users.js
import React from 'react';
import { useGetUsersQuery } from './services/apiSlice';

const Users = () => {
  const { data: users, error, isLoading } = useGetUsersQuery();

  if (isLoading) return <p>Loading...</p>;
  if (error) return <p>Error: {error.message}</p>;

  return (
    <ul>
      {users.map((user) => (
        <li key={user.id}>{user.name}</li>
      ))}
    </ul>
  );
};

export default Users;


Component
  ↓
useGetUsersQuery()
  ↓
RTK Query (handles API request, loading, error)
  ↓
Redux store (caches result)

Benefits of RTK Query
Feature	                Benefit
Auto caching	        No need to manually manage state
Refetching control	    Re-fetch on focus or interval
Auto invalidation	    Keeps data fresh after updates
Code generation	        Custom hooks like useGetUsersQuery()







