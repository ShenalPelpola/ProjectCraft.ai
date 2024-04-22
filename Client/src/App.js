import { SideBar } from './Components/ChatComponents/SideBar/SideBar';
import { ChatContainer } from './Components/ChatComponents/ChatView/ChatView';
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