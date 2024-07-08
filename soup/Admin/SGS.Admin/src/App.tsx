import { useEffect, useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { useBuilderQuery } from './hooks';
import { IBuilderQuery } from './interfaces';

function App() {
  const [count, setCount] = useState(0)
  const [requestQuery, setRequestQuery] = useState<IBuilderQuery>(
    {
      appendQueryOr: [
        { key: "state", value: 'PROCESSING', operator: '$eq' },
        { key: "state", value: 'SHIPPING', operator: '$eq' },
      ],
      appendQueryAnd: [
        { key: "quantity", value: "0", operator: '$gt' },
        { key: "categoryId", value: 'Cate1', operator: '$neq' },
      ],
      toPaging: {
        page: 1,
        pageSize: 100
      },
      toJoin: ['categories', 'orders']
    }
  );

  const query = useBuilderQuery(requestQuery);

  useEffect(() => {
    console.log(query);
  }, [requestQuery]);

  return (
    <>
      <div>
        <a href="https://vitejs.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={() => {
          setCount((count) => count + 1);
          setRequestQuery({
            appendQueryAnd: [
              { key: "quantity", value: Math.random().toString(), operator: '$gt' },
              { key: "categoryId", value: Math.random().toString(), operator: '$neq' },
            ],
            toPaging: {
              page: 2,
              pageSize: 25
            },
            toJoin: ['categories']
          });
        }}>
          count is {count}
        </button>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
    </>
  )
}

export default App
