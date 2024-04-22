import { Button } from '../../CustomUis/Button/Button';
import logo from '../../../assets/chatgpt.svg';
import addBtn from '../../../assets/add-30.png';
import msgIcon from '../../../assets/message.svg';
import home from '../../../assets/home.svg';
import saved from '../../../assets/bookmark.svg';
import './SideBar.scss';

export const SideBar = () => {
    return (
        <div className="side-bar">
            <div className="upper">
                <div className="upper-top">
                    <img src={logo} alt="logo" className="logo" />
                    <span className="brand">ProjectCraft.ai</span>
                </div>
                <button className="create-project-btn">
                    <img src={addBtn} alt="new project" className="add-icon" />
                    <span>New Project</span>
                </button>
                <div className="upper-bottom">
                    <Button className="query">
                        <img src={msgIcon} alt="query" className="query-image" /> What is Programming?
                    </Button>
                    <Button className="query">
                        <img src={msgIcon} alt="query" className="query-image" />What is Programming?
                    </Button>
                </div>
            </div>
            <div className="lower">
                <div className="list-items"><img src={home} alt="home" />Home</div>
                <div className="list-items"><img src={saved} alt="save" />Saved</div>
            </div>
        </div>
    )
};