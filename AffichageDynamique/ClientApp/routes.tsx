import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Accueil } from './components/Accueil';
import { RechercheEntreprises } from './components/RechercheEntreprises';
import { Partenaires } from './components/Partenaires';
import { Evenements } from './components/Evenements';
//import { EditEntreprises } from './components/EditEntreprises';
import { Apropos } from './components/Apropos';
import { Presentation } from './components/Presentation';

export const routes = <Layout>
    <Route exact path='/' component={ Accueil } />
    <Route path='/presentation' component={Presentation } />
    <Route path='/rechercheentreprises' component={RechercheEntreprises} />
    <Route path='/partenaires' component={Partenaires} />
    <Route path='/evenements' component={Evenements} />
    <Route path='/apropos' component={Apropos} />
    {/*<Route path='/editentreprises' component={EditEntreprises} />*/}
</Layout>;
