import { useToggle } from "@uidotdev/usehooks";
import { SideBar } from './Components/ChatComponents/SideBar/SideBar';
import { ChatContainer } from './Components/ChatComponents/ChatView/ChatView';
import './App.css';
import { Empties } from './Constants/Empties';

function App() {
  const [isCreateWindowOpen, toggleCreateWindow] = useToggle(Empties.defaultBoolean);

  return (
    <div className="App">
      <SideBar toggleCreateWindow={toggleCreateWindow} />
      <div className="main">
        <ChatContainer isCreateWindowOpen={isCreateWindowOpen} toggleCreateWindow={toggleCreateWindow}/>
      </div>
    </div>
  );
}

export default App;