import React from 'react';
import PropTypes from 'prop-types';
import { formatDate } from '@fullcalendar/core';
import { useNavigate } from 'react-router-dom';
import hasty from '../../assets/images/hastylogo.png';
import Ratings from '../rating/Ratings';
import * as ratingsService from '../../services/ratingsService';
import './blogstyles.css';

function Blog({ currentUser, blog: aBlog, onDeleteClicked }) {
  const stateForDisplay = { type: 'BLOG_DISPLAY', payload: aBlog };
  const navigate = useNavigate();

  const onEditClicked = (event) => {
    event.preventDefault();
    navigate(`/blogs/${aBlog.id}/edit`, { state: stateForDisplay });
  };

  const onDelete = (event) => {
    event.preventDefault();
    onDeleteClicked(aBlog, event);
  };

  let date = formatDate(aBlog.datePublished, {
    month: 'numeric',
    year: 'numeric',
    day: 'numeric',
  });

  const onBlogClicked = (event) => {
    event.preventDefault();

    navigate(`/blogs/${aBlog.id}/details`, { state: stateForDisplay });
  };

  return (
    <div className="col-md-3 mb-2 mt-2">
      <div className="d-block card blog-card border border-secondary h-100" id={aBlog.id} onClick={onBlogClicked}>
        <img src={aBlog.imageUrl} className="card-img blog-img" alt="I Love Code" />
        <div className="card-body">
          <h5 className="card-title d-flex justify-content-center">{aBlog.title}</h5>
          <p className="card-title d-flex justify-content-center">
            {aBlog.author.firstName} {aBlog.author.mi} {aBlog.author.lastName}
          </p>
          <p className="d-flex justify-content-around">{aBlog.blogType.name}</p>
          <p className="d-flex justify-content-around">{date}</p>
          <p className="text-center">
            <Ratings isReadOnly={true} entityId={aBlog.id} entityTypeId={ratingsService.entityTypes.BLOG}></Ratings>
          </p>

          {(currentUser.id === 71 || currentUser.id === aBlog.author.id) && (
            <button type="submit" className="btn btn-secondary mx-1" onClick={onEditClicked}>
              Edit
            </button>
          )}
          {(currentUser.id === 71 || currentUser.id === aBlog.author.id) && (
            <button type="submit" className="btn btn-secondary mt-2 mx-1" onClick={onDelete}>
              Delete
            </button>
          )}
          {(currentUser.id === 71 || currentUser.id === aBlog.author.id) && aBlog.isPublished && (
            <img className="h-25 w-25 mt-1" src={hasty} alt="isPublished logo" />
          )}
        </div>
      </div>
    </div>
  );
}

Blog.propTypes = {
  blog: PropTypes.shape({
    imageUrl: PropTypes.string,
    title: PropTypes.string.isRequired,
    author: PropTypes.shape({
      firstName: PropTypes.string.isRequired,
      mi: PropTypes.string,
      lastName: PropTypes.string.isRequired,
      id: PropTypes.number.isRequired,
    }).isRequired,
    subject: PropTypes.string,
    datePublished: PropTypes.string,
    id: PropTypes.number.isRequired,
    isPublished: PropTypes.bool.isRequired,
    blogType: PropTypes.shape({
      id: PropTypes.number.isRequired,
      name: PropTypes.string.isRequired,
    }).isRequired,
    blogStatus: PropTypes.shape({
      id: PropTypes.number.isRequired,
      name: PropTypes.string.isRequired,
    }).isRequired,
  }).isRequired,
  onDeleteClicked: PropTypes.func,
  currentUser: PropTypes.shape({
    id: PropTypes.number,
    isLoggedIn: PropTypes.bool,
  }),
  isReadOnly: PropTypes.bool.isRequired,
};

export default Blog;
