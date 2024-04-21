import { SideBar } from './Components/ChatView/SideBar/SideBar';
import { ChatContainer } from './Components/ChatView/ChatContainer/ChatContainer';
import './App.css';

function App() {
  return (
    <div className="App">
      <SideBar />
      <div className="main">
        <ChatContainer />
      </div>
    </div>
  );
}

export default App;