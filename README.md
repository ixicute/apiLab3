# School Project - Web RESTful API

### This was an API that was created for a school project. The following is the requirements provided:

## ⚙️ **Application/Database**

The first thing you should create is a very basic application with a database that can handle the following:

- [x] It should be possible to store people with basic information about them, such as name and phone number.
- [x] The system should be able to store an unlimited number of interests that a person has. Each interest should have a title and a short description.
- [x] Each person can be associated with any number of interests.
- [x] It should be possible to store an unlimited number of links (to websites) for each interest of each person. So, if a person adds a link, it is associated with both that person and the interest.


## 🗣 **Create a REST API**

The second step is to create a REST API that allows external services to make the following requests to your API and implement these changes in your application.

- [x] Get all the people in the system.
- [x] Get all the interests associated with a specific person.
- [x] Get all the links associated with a specific person.
- [x] Associate a person with a new interest.
- [x] Add new links for a specific person and a specific interest.

---

## **Extra Challenge (optional)**

- [x] Provide the ability for the API caller, when requesting a person, to directly retrieve all the interests and links for that person in a hierarchical JSON file.
- [x] Allow the API caller to filter the retrieved data, similar to a search. For example, if I include "to" in the request to retrieve all people, I want to receive those with "to" in their names, such as "tobias" or "tomas". You can implement this for all the requests if you like.
- [ ] Implement pagination for the requests. For example, when requesting people, I might get the first 100 people and then make additional requests to get more. It would be nice if the request also specifies the number of people to be returned in a single request, so I can choose to receive, let's say, 10 if I only want that many.

#
![ApiDemo](https://github.com/ixicute/apiLab3/assets/25350208/f449b24b-7902-43bf-bab3-a560e07c3dbb)


## The following is the api-calls for each endpoint:

# GET-requests

### This link will get you all users. if you want to filter the results based on the name add the "?filter=" and a string value like "a".
https://localhost:7248/api/User?filter=a

### The following link will get a list of user interests based on the parameter "userId" which should be the id of the requested user:
https://localhost:7248/api/User/GetUserInterests?userId=1

### The following link works the same as the one methond above but responds with a list of "links" that are connected to the requested user:
https://localhost:7248/api/User/GetUserLinks?userId=1

### The following gets all data about the user. The name, list of interests and list of links that are connected to the requested user:
https://localhost:7248/api/User/GetUserDataById?userId=1


# POST-requests
### With this link you can create a new user with the request-body below the link:
https://localhost:7248/api/User/AddUser

{
  "firstName": "string",
  "lastName": "string",
  "phoneNumber": "string"
}
#### NOTE: The code will deny the post-request if any of the values is set to "string".

### With this link you can create a new interest that will be connected to the user based on the userId in the request-body:
https://localhost:7248/api/User/AddNewInterest
{
  "userId": 0,
  "interestTitle": "string",
  "interestDescription": "string"
}

### With this link you can create a new "link" that will be connected to the interest that is connected to the specified user. Both id's are required in the request-body:
https://localhost:7248/api/User/AddNewLink
{
  "userId": 0,
  "interestId": 0,
  "link": "string"
}


# DELETE-request
## Finally we have the DELETE-request that will remove a user based on the id. Note that this will also remove all links and interests connected to the user but will not remove the interests that are in the seperate interests table.
https://localhost:7248/api/User/RemoveUser?userId=1
