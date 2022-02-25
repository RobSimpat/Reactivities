import React, { useEffect } from 'react';
import Nav from './components/nav';
import { BrowserRouter, Route, Switch, useLocation } from 'react-router-dom';
import LoginForm from './features/users/LoginForm';
import { Container } from 'semantic-ui-react';
import HomePage from './features/home/homePage';
import { useStore } from './app/stores/store';
import { observer } from 'mobx-react-lite';
import LoadingComponent from './components/LoadingComponent';
import ModalContainer from './app/common/modals/ModalContainer';
import RegisterForm from './features/users/RegisterForm';
import { ToastContainer } from 'react-toastify';



function App() {
  const location = useLocation();
  const {commonStore, userStore} = useStore();

  useEffect(()=> {
    if(commonStore.token){
      userStore.getUser().finally(()=> commonStore.setAppLoaded());
    }else{
      commonStore.setAppLoaded();
    }
  }, [commonStore, userStore])

  if(!commonStore.appLoaded) return<LoadingComponent content='Loading app...'/>

  return (
    <>
    <ToastContainer position='bottom-right' hideProgressBar />
    <ModalContainer />
    <Route exact path="/" component={HomePage} />
    <Route
      path={'/(.+)'}
      render={() => (
        <>
        <Nav />
          <Container style={{ marginTop: '7em' }}>
            <Switch>
              <Route exact path="/login" component={LoginForm} />
              <Route exact path="/register" component={RegisterForm} />
              
            
            </Switch>     
        </Container>

        </>

      )}
    />
    </>
    

      );
}

export default observer (App);
