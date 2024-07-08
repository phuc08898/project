import './App.css'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import { Billboard, Startup } from './pages'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/billboard" element={<Billboard />} />
        <Route path="/startup" element={<Startup />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App;
