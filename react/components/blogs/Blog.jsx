import React from 'react';
import Ratings from '../rating/Ratings';
import * as ratingsService from '../../services/ratingsService';


function Blog({ currentUser, blog: aBlog, onDeleteClicked }) {

          <p className="text-center">
            <Ratings isReadOnly={true} entityId={aBlog.id} entityTypeId={ratingsService.entityTypes.BLOG}></Ratings>
          </p>

export default Blog;
