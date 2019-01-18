# MVC 5 with Google and Facebook Authorization Enabled

I took on this tutorial project to further my understanding of creating secure login structures for websites.  The tutorial, however, glossed over how to store app secrets in a seperate file, merely stating that it shouldn't be hard coded while proceeding to hard code it.  So I had to do extra research to make it work with the secrets obscured.  I did not include the secrets file.  It had to be created within the project level folder as IIS Express cannot see anything outside of that for testing.  It took me a while to figure that out!
