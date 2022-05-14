# Lab 2 – Adding external analyzers

In this lab, we’re going to continue improving the code file that we
started in Lab1. This time, we’re going to add a couple open source
analyzer packages and see what additional improvements they can
recommend for our code. Begin by opening the Lab2-Start.project file.

Right click on the project and select “Manage NuGet Packages”. Click on
the Browse tab and search for “Code-cracker”. Locate the
codecracker.CSharp package and install it. Do the same for the
StyleCop.Analyzers package.

<img src="media/Lab2-image1.png" style="width:6.5in;height:4.275in" />

<img src="media/Lab2-image2.png" style="width:2.5in;height:1.67708in" />If
you expand the references tab of your project, you should see a folder
for “Analyzers” and under that entries for CodeCracker.CSharp and
StyleCop.Analyzers. Analyzers need to be installed on each project that
you wish to use them on. This allows for easy deployment via the NuGet
package manager. It also allows library authors to ship analyzers with
their libraries when domain specific or best-practice issues are known.

Even without building your project, you should notice that Visual Studio
is now reporting quite a number of new errors, warnings, and info
messages. Use the table below to guide you through locating the various
issues and applying the appropriate code fixes for this lab. In many
cases, the option you choose may be subject to your preferred coding
standards, but if you select different fixes than recommended here, the
subsequent fixes may not light up as some fixes are dependent on
applying the previous fix.

To illustrate the first row of the table, find the file Class1 on the
“Person” string of the “Person class” to apply the fix “Move type to new
file”, you should see the following in Visual Studio 2017. Applying the
fix will apply the best practice rule of limiting each file to a single
class and generates a new Person.cs file.

<img src="media/Lab2-image3.png" style="width:6.5in;height:3.54861in" />

Here are the remainder of the code fixes for this lab. See how many of
them you can apply.

<table>
<colgroup>
<col style="width: 10%" />
<col style="width: 25%" />
<col style="width: 18%" />
<col style="width: 45%" />
</colgroup>
<thead>
<tr class="header">
<th>File</th>
<th>Code block</th>
<th>Click on</th>
<th>Apply fix</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td rowspan="14">Class1</td>
<td>Person class</td>
<td>Person</td>
<td>Move type to new file</td>
</tr>
<tr class="even">
<td>ToDispose class</td>
<td>ToDispose</td>
<td>Move type to new file</td>
</tr>
<tr class="odd">
<td rowspan="2">using block</td>
<td rowspan="2">using System</td>
<td>Remove unnecessary usings</td>
</tr>
<tr class="even">
<td>Reorder usings</td>
</tr>
<tr class="odd">
<td>Namespace block</td>
<td>namespace</td>
<td>Add file header</td>
</tr>
<tr class="even">
<td>Class1 declaration</td>
<td>Class1</td>
<td>Order Class1’s members following StyleCop patterns</td>
</tr>
<tr class="odd">
<td rowspan="4">GetJim method</td>
<td rowspan="3">var Person</td>
<td>Use object initializer</td>
</tr>
<tr class="even">
<td>Inline temporary variable</td>
</tr>
<tr class="odd">
<td>Convert to expression bodied member</td>
</tr>
<tr class="even">
<td>GetJim</td>
<td>Replace “GetJim” with property</td>
</tr>
<tr class="odd">
<td rowspan="4">ShouldUseVar method – Throw new argumentException</td>
<td>“input”</td>
<td>use nameof()</td>
</tr>
<tr class="even">
<td>Person person</td>
<td><p>Use var</p>
<blockquote>
<p>(Note: The above action will cause a conflict with the stylecop rule.
Disable the stylecop rule in the ruleset properties.)</p>
</blockquote></td>
</tr>
<tr class="odd">
<td>His parent is {0}</td>
<td>Change to string interpolation</td>
</tr>
<tr class="even">
<td>Catch</td>
<td>Remove wrapping Try Block</td>
</tr>
<tr class="odd">
<td rowspan="9">Person</td>
<td>_name field declaration</td>
<td>_name</td>
<td>Use auto property</td>
</tr>
<tr class="even">
<td rowspan="2">CanVote method</td>
<td rowspan="2">If</td>
<td>Change to ternary</td>
</tr>
<tr class="odd">
<td>Convert to expression bodied member</td>
</tr>
<tr class="even">
<td></td>
<td>bool</td>
<td>Document return value</td>
</tr>
<tr class="odd">
<td></td>
<td>&lt;returns&gt;</td>
<td>Change to &lt;returns&gt;if person is of voting age</td>
</tr>
<tr class="even">
<td>Blog field declaration</td>
<td>“thinqlinq”</td>
<td>Manually change the text to <a
href="http://www.thinqlinq.com">http://www.thinqlinq.com</a> (a great
site if I could be so modest). Note here, there is no fix. This is just
an analyzer error message because it detects a runtime exception for a
domain specific issue.</td>
</tr>
<tr class="odd">
<td>IsPrime method</td>
<td>Where</td>
<td>Remove ‘Where’ moving predicate to ‘Any’</td>
</tr>
<tr class="even">
<td>IsFibber method</td>
<td>First “if”</td>
<td>Convert to switch</td>
</tr>
<tr class="odd">
<td>SayHello method</td>
<td>hello +=</td>
<td>Use StringBuilder to create a value for ‘hello’</td>
</tr>
</tbody>
</table>

## On your own

Try working through the rest of the errors, warnings and info messages
and see if you can either clean up all of the remaining issues, or
suppress/disable those that you don’t feel are necessary for your
application/agency.
